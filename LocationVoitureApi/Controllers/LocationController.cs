using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Location>>> getAll()
        {
            using (var context = new projetContext())
            {
                return context.Locations.ToList();
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Location>> get(int id)
        {
            using (var context = new projetContext())
            {
                var a = await context.Locations.FindAsync(id);
                if (a == null) 
                    return NotFound("Location not found"); 
                return Ok(a);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Location>>> addAdmin(Location a)
        {
            using (var context = new projetContext())
            {
                context.Locations.Add(a);
                await context.SaveChangesAsync();
                return Ok(context.Locations.ToList());
            }

        }


        [HttpPut]
        public async Task<ActionResult<Location>> update(Location a)
        {
            using (var context = new projetContext())
            {
                context.Locations.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Location>>> delete(int id)
        {
            using (var context = new projetContext())
            {

                var a = await context.Locations.FindAsync(id);
                if (a == null)
                    return NotFound("Location not found");
                
                context.Locations.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Locations.ToList());
            }

        }

    }
}
