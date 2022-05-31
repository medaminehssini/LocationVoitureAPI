using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class MarqueController : ControllerBase
    {
        private projetContext context;

        public MarqueController(projetContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Marque>>> getAll()
        {
            {
                return context.Marques.ToList();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marque>> get(int id)
        {
            {
                var a = await context.Marques.FindAsync(id);
                if (a == null)
                    return NotFound("Marque not found");
                return Ok(a);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Marque>>> addAdmin(Marque a)
        {
            {
                context.Marques.Add(a);
                await context.SaveChangesAsync();
                return Ok(context.Marques.ToList());
            }
        }


        [HttpPut]
        public async Task<ActionResult<Marque>> update(Marque a)
        {
            {
                context.Marques.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Marque>>> delete(int id)
        {
            {
                var a = await context.Marques.FindAsync(id);
                if (a == null)
                    return NotFound("Marque not found");

                context.Marques.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Marques.ToList());
            }
        }
    }
}