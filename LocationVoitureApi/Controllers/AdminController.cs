using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Admin>>> getAdmins()
        {
            using (var context = new projetContext())
            {
                return context.Admins.ToList();
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Admin>> getAdmin(int id)
        {
            using (var context = new projetContext())
            {
                var a = await context.Admins.FindAsync(id);
                if (a == null) 
                    return NotFound("Admin not found"); 
                return Ok(a);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Admin>>> addAdmin(Admin a)
        {
            using (var context = new projetContext())
            {
                context.Admins.Add(a);
                await context.SaveChangesAsync();
                return Ok(context.Admins.ToList());
            }

        }


        [HttpPut]
        public async Task<ActionResult<Admin>> updateAdmin(Admin a)
        {
            using (var context = new projetContext())
            {
                context.Admins.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Admin>>> deleteAdmin(int id)
        {
            using (var context = new projetContext())
            {

                var a = await context.Admins.FindAsync(id);
                if (a == null)
                    return NotFound("Admin not found");
                
                context.Admins.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Admins.ToList());
            }

        }

    }
}
