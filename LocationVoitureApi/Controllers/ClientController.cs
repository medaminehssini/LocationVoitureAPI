using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LocationVoitureApi.Helpers;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        projetContext _context;
        IConfiguration _configuration;

        public ClientController(projetContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet]
        [Authorize(Roles = "employer , admin")]
        public async Task<ActionResult<List<Client>>> getAll()
        {
            {
                return _context.Clients.ToList();
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employer , admin , client")]
        public async Task<ActionResult<Client>> get(int id)
        {
            var a = await _context.Clients.FindAsync(id);
            if (a == null)
                return NotFound("Client not found");


            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role == "employer" || role == "admin" || (Int32.Parse(userId) == id))
                return Ok(a);
            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<List<Client>>> add(Client a)
        {
            _context.Clients.Add(a);
            await _context.SaveChangesAsync();
            return Ok(_context.Clients.ToList());
        }


        [HttpPut]
        [Authorize(Roles = "employer , user")]
        public async Task<ActionResult<Client>> update(Client a)
        {
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role == "employer" || (Int32.Parse(userId) == a.Id))
            {
                _context.Clients.Update(a);
                await _context.SaveChangesAsync();
                return Ok(a);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "employer ")]
        public async Task<ActionResult<List<Client>>> delete(int id)
        {
            {
                var a = await _context.Clients.FindAsync(id);
                if (a == null)
                    return NotFound("Clienr not found");

                _context.Clients.Remove(a);
                await _context.SaveChangesAsync();
                return Ok(_context.Clients.ToList());
            }
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<String>> login([FromForm] UserLogin user)
        {
            Client client = _context.Clients.Where(a => a.Email == user.Email & a.Password == user.Password)
                .FirstOrDefault();
            if (client == null)
                return BadRequest("Login or password uncorrect");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.Email, client.Email),
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
            };


            return Ok(JWT.createToken(claims, _configuration));
        }
    }
}