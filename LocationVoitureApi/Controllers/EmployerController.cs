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
    [Authorize(Roles = "admin")]
    public class EmployerController : ControllerBase
    {
        projetContext _context;
        IConfiguration _configuration;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IUpload upload;

        public EmployerController(projetContext context, IConfiguration configuration,
            IWebHostEnvironment hostEnvironment, IUpload upload)
        {
            _context = context;
            _configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            this.upload = upload;
        }


        [HttpGet]
        public async Task<ActionResult<List<Employer>>> getAll()
        {
            {
                return _context.Employers.ToList();
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "employer , admin")]
        public async Task<ActionResult<Client>> get(int id)
        {
            var role = this.User.FindFirstValue(ClaimTypes.Role);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var a = await _context.Employers.FindAsync(id);
            if (a == null)
                return NotFound("Employer not found");

            if (role == "admin" || (Int32.Parse(userId) == id))

                return Ok(a);


            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<List<Employer>>> add([FromForm] EmployerUpload a)
        {
            string s = upload.upload(a.photo, "Images/Employer");

            if (s != null)
            {
                Employer employer = new Employer();

                employer.Photo = s;
                employer.Idagence = a.Idagence;
                employer.Password = a.Password;
                employer.Nom = a.Nom;


                _context.Employers.Add(employer);
                await _context.SaveChangesAsync();
                return Ok(_context.Employers.ToList());
            }

            return BadRequest("R");
        }


        [HttpPut]
        [Authorize(Roles = "employer , admin")]
        public async Task<ActionResult<Employer>> update([FromForm] EmployerUpload a)
        {
            bool verif = false;
            if (a.photo != null)
                verif = true;
            string s = upload.upload(a.photo, "Images/Employer");
            if (s != null)
            {
                var role = this.User.FindFirstValue(ClaimTypes.Role);
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Employer employer = new Employer();
                employer.Id = a.Id;
                if (verif)
                    employer.Photo = s;
                employer.Idagence = a.Idagence;
                employer.Password = a.Password;
                employer.Nom = a.Nom;


                if (role == "admin" || (Int32.Parse(userId) == a.Id))
                {
                    _context.Employers.Update(employer);
                    await _context.SaveChangesAsync();
                    return Ok(a);
                }
            }

            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employer>>> delete(int id)
        {
            {
                var a = await _context.Employers.FindAsync(id);
                if (a == null)
                    return NotFound("Clienr not found");

                _context.Employers.Remove(a);
                await _context.SaveChangesAsync();
                return Ok(_context.Employers.ToList());
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<String>> login([FromForm] UserLogin user)
        {
            Employer employer = _context.Employers.Where(a => a.Nom == user.Email & a.Password == user.Password)
                .FirstOrDefault();
            if (employer == null)
                return BadRequest("Login or password uncorrect");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "employer"),
                new Claim(ClaimTypes.Email, employer.Nom),
                new Claim(ClaimTypes.NameIdentifier, employer.Id.ToString()),
            };


            return Ok(JWT.createToken(claims, _configuration));
        }
    }
}