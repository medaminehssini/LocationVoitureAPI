using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Employer>>> getAll()
        {
            using (var context = new projetContext())
            {
                return context.Employers.ToList();
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Client>> get(int id)
        {
            using (var context = new projetContext())
            {
                var a = await context.Employers.FindAsync(id);
                if (a == null) 
                    return NotFound("Employer not found"); 
                return Ok(a);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Employer>>> addAdmin(Employer a)
        {
            using (var context = new projetContext())
            {
                context.Employers.Add(a);
                await context.SaveChangesAsync();
                return Ok(context.Employers.ToList());
            }

        }


        [HttpPut]
        public async Task<ActionResult<Employer>> update(Employer a)
        {
            using (var context = new projetContext())
            {
                context.Employers.Update(a);
                await context.SaveChangesAsync();
                return Ok(a);
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employer>>> delete(int id)
        {
            using (var context = new projetContext())
            {

                var a = await context.Employers.FindAsync(id);
                if (a == null)
                    return NotFound("Clienr not found");
                
                context.Employers.Remove(a);
                await context.SaveChangesAsync();
                return Ok(context.Employers.ToList());
            }

        }

    }
}
