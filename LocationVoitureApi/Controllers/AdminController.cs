using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocationVoitureApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using LocationVoitureApi.Helpers;

namespace LocationVoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class AdminController : ControllerBase
    {

        private projetContext _context;
        private readonly IConfiguration configuration;
        private readonly IUpload upload;

        public AdminController(projetContext context , IConfiguration configuration , IUpload upload)
        {
            _context = context;
            this.configuration = configuration;
            this.upload = upload;
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

        public async Task<ActionResult<List<Admin>>> add([FromForm]AdminUpload a)
        {


            string s = upload.upload(a.Photo, "Images/Admin");

            if (s != null)
            {

                Admin admin = new Admin();
                admin.Photo = s;
                admin.Nom = a.Nom;
                admin.Email = a.Email;
                admin.Password = a.Password;


                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();
                return Ok(_context.Admins.ToList());
            }
            return BadRequest();   
        }


        [HttpPut]
        public async Task<ActionResult<Admin>> update([FromForm]AdminUpload a)
        {

            Admin admin = _context.Admins.Find(a);
            if (admin != null)
            {
               
             
                admin.Nom = a.Nom;
                admin.Email = a.Email;
                admin.Password = a.Password;
                string s = null;
                
                if (a.Photo != null)
                {
                     s = upload.upload(a.Photo, "Images/Admin");
                    if(s != null)
                    {
                        admin.Photo = s;
                    }
                }
                

                _context.Admins.Update(admin);
                await _context.SaveChangesAsync();
                return Ok(a);
            }
            else
                return NotFound("admin not found");

          
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<String>> login ([FromForm]UserLogin user)
        {

            Admin admin = _context.Admins.Where(a => a.Email == user.Email ).FirstOrDefault();
            if (admin == null)
               return BadRequest("Login or password uncorrect");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,"admin") ,
                new Claim(ClaimTypes.Email,admin.Email),
                new Claim(ClaimTypes.NameIdentifier,admin.Id.ToString()),

            };
            

            return Ok(JWT.createToken(claims , configuration) );
        }




    }
}
