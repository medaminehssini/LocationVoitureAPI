using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoitureController : ControllerBase
    {
        projetContext context;


        public VoitureController(projetContext projetContext)
        {
            context = projetContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Voiture>>> getAll()
        {
            {
                return context.Voitures.Include("Locations").ToList();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Voiture>> get(int id)
        {
            {
                var a = await context.Voitures.FindAsync(id);
                if (a == null)
                    return NotFound("Voiture not found");
                return Ok(a);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Voiture>>> addAdmin(Voiture a)
        {
            {
                context.Voitures.Add(a);
                await context.SaveChangesAsync();
                return Ok(context.Voitures.ToList());
            }
        }


        [HttpPut]
        public async Task<ActionResult<Voiture>> update(Voiture a)
        {
            {
                context.Voitures.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Voiture>>> delete(int id)
        {
            {
                var a = await context.Voitures.FindAsync(id);
                if (a == null)
                    return NotFound("Voiture not found");

                context.Voitures.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Voitures.ToList());
            }
        }


        [HttpGet("dispo")]
        public async Task<ActionResult<List<Voiture>>> getDispo()
        {
            DateTime sqlFormattedDate = DateTime.Now;
            var location = context.Locations.Where(x => x.DateDeb <= sqlFormattedDate & x.DateFin >= sqlFormattedDate)
                .Select(x => x.VoitureMatricule).ToArray();
            var a = await context.Voitures.Where(x => !location.Contains(x.Matricule)).ToListAsync();
            if (a == null)
                return NotFound("Voiture not found");
            return Ok(a);
        }
    }
}