using LocationVoitureApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ("admin"))]
    public class AgenceController : ControllerBase
    {
        public projetContext context { get; }

        public AgenceController(projetContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Agence>>> getAdmins()
        {
            {
                return context.Agences.ToList();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agence>> getAdmin(int id)
        {
            {
                var a = await context.Agences.FindAsync(id);
                if (a == null)
                    return NotFound("Admin not found");
                return Ok(a);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Agence>>> addAdmin([FromForm] Agence a)
        {
            {
                context.Agences.Add(a);

                await context.SaveChangesAsync();

                return Ok(context.Agences.ToList());
            }
        }


        [HttpPut]
        public async Task<ActionResult<Agence>> updateAdmin([FromForm] Agence a)
        {
            {
                context.Agences.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Agence>>> deleteAdmin(int id)
        {
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