using System.IdentityModel.Tokens.Jwt;
using System.Text;
using _7__WebAPIApp.DTO;
using _7__WebAPIApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiApp.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace _7__WebAPIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IConfiguration _configuration;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new AppUser
            {
                UserName = model.Username,
                FullName = model.FullName!,
                Email = model.Email,
                DateAdded = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
                return StatusCode(201);
            else
                return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDTO.Email!);

            if (user == null)
            {
                return BadRequest(new { message = "Email is incorrect" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password!, false);
            if (result.Succeeded)
            {
                return Ok(new { token = GenerateJWT(user) });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerateJWT(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "turko.com"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}