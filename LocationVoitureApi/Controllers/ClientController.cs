using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Client>>> getAll()
        {
            using (var context = new projetContext())
            {
                return context.Clients.ToList();
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Client>> get(int id)
        {
            using (var context = new projetContext())
            {
                var a = await context.Clients.FindAsync(id);
                if (a == null) 
                    return NotFound("Client not found"); 
                return Ok(a);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> addAdmin(Client a)
        {
            using (var context = new projetContext())
            {
                context.Clients.Add(a);
                await context.SaveChangesAsync();
                return Ok(context.Clients.ToList());
            }

        }


        [HttpPut]
        public async Task<ActionResult<Client>> update(Client a)
        {
            using (var context = new projetContext())
            {
                context.Clients.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Client>>> delete(int id)
        {
            using (var context = new projetContext())
            {

                var a = await context.Clients.FindAsync(id);
                if (a == null)
                    return NotFound("Clienr not found");
                
                context.Clients.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Clients.ToList());
            }

        }

    }
}
