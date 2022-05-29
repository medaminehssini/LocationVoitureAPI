using LocationVoitureApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenceController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Agence>>> getAdmins()
        {
            using (var context = new projetContext())
            {
                return context.Agences.ToList();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agence>> getAdmin(int id)
        {
            using (var context = new projetContext())
            {
                var a = await context.Agences.FindAsync(id);
                if (a == null)
                    return NotFound("Admin not found");
                return Ok(a);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Agence>>> addAdmin(Agence a)
        {
            using (var context = new projetContext())
            {
                context.Agences.Add(a);
                await context.SaveChangesAsync();
                return Ok(context.Agences.ToList());
            }

        }


        [HttpPut]
        public async Task<ActionResult<Agence>> updateAdmin([FromForm] Agence a)
        {
            using (var context = new projetContext())
            {
                context.Agences.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Agence>>> deleteAdmin(int id)
        {
            using (var context = new projetContext())
            {

                var a = await context.Agences.FindAsync(id);
                if (a == null)
                    return NotFound("Agence not found");

                context.Agences.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Agences.ToList());
            }

        }
    }
}
