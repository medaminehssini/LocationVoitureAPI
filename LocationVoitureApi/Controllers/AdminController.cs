using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {

        private projetContext _context;
        private readonly IConfiguration configuration;

        public AdminController(projetContext context , IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Admin>>> getAdmins()
        {
            return _context.Admins.ToList();
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Admin>> getAdmin(int id)
        {

            var a = await _context.Admins.FindAsync(id);
            if (a == null) 
                return NotFound("Admin not found"); 
            return Ok(a);

        }

        [HttpPost]
        public async Task<ActionResult<List<Admin>>> addAdmin(Admin a)
        {

            _context.Admins.Add(a);
            await _context.SaveChangesAsync();
            return Ok(_context.Admins.ToList());
        }


        [HttpPut]
        public async Task<ActionResult<Admin>> updateAdmin(Admin a)
        {
            _context.Admins.Update(a);
            await _context.SaveChangesAsync();
            return Ok(a);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Admin>>> deleteAdmin(int id)
        {


            var a = await _context.Admins.FindAsync(id);
            if (a == null)
                return NotFound("Admin not found");
                
            _context.Admins.Remove(a);
            await _context.SaveChangesAsync();
            return Ok(_context.Admins.ToList());
            

        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<ActionResult<String>> login (UserLogin user)
        {

            Admin admin = _context.Admins.Where(a => a.Email == user.Email ).FirstOrDefault();
            if (admin == null)
               return BadRequest("Login or password uncorrect");

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"admin") ,
                new Claim(ClaimTypes.Email,admin.Email),
            };

            return Ok("fdsq");
        }


        private string createToken (List<Claim> claim)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Token").Value);
            var cred = new SigningCredentials(key , SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claim,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: cred);

            return "";
        }

    }
}
