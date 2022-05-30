using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        projetContext context; 


        public LocationController (projetContext projetContext)
        {
            context = projetContext;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Location>>> getAll()
        {
            List<Location> locations ;
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId =  Int32.Parse( this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (role == "admin")
            {
                locations = context.Locations.ToList();
            }
            else if (role == "employer")
            {
                locations = context.Locations.Where(l => l.Idemployeur == userId ).ToList();

            }
            else
            {
                locations = context.Locations.Where(l => l.IdClient == userId).ToList();
            }


            return locations ;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Location>> get(int id)
        {

            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Location location;
            if(role == "admin")
            {
                location = await context.Locations.FindAsync(id);
            }else if (role == "employer")
            {
                location = context.Locations.Where(l => l.Idemployeur == userId && l.Id == id).FirstOrDefault();
            }else
            {
                location = context.Locations.Where(l => l.IdClient == userId && l.Id == id).FirstOrDefault();
            }

            if (location == null) 
                    return NotFound("Location not found"); 
                return Ok(location);
        }

        [HttpPost]
        [Authorize(Roles =("employer , user"))]
        public async Task<ActionResult<List<Location>>> add(Location a)
        {
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (role == "employer")
            {
                a.Idemployeur = userId;
            }else
            {
                a.IdClient = userId;
            }
            context.Locations.Add(a);
            await context.SaveChangesAsync();
            return Ok(a);
        }


        [HttpPut]
        [Authorize(Roles = ("employer , user"))]
        public async Task<ActionResult<Location>> update(Location a)
        {

            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));


            if((role == "employer" & a.Idemployeur != userId ) || (role == "user" & a.IdClient != userId))
            {

                context.Locations.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }

            return StatusCode(401);




        }


        [HttpDelete("{id}")]
        [Authorize(Roles = ("employer , user"))]
        public async Task<ActionResult<List<Location>>> delete(int id)
        {

            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var a = await context.Locations.FindAsync(id);
            if (a == null)
                return NotFound("Location not found");

            if ((role == "employer" & a.Idemployeur != userId) || (role == "user" & a.IdClient != userId))
            {
              

                context.Locations.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Locations.ToList());
            }
            else
                return StatusCode(401);

        }





    }
}
