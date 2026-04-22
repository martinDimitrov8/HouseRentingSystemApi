using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HouseRentingSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid login data",
                    errors = ModelState
                });
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            var token = GenerateJwtToken(user);
            return Ok(new
            {
                message = "Login successful",
                token
            });

        }
        [HttpPost("register")]
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid register data",
                    errors = ModelState
                });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                return BadRequest(new
                {
                    message = "Email already exists",
                });
            }

            var existingByUsername = await _userManager.FindByNameAsync(model.Username);
            if (existingByUsername != null)
            {
                return BadRequest(new
                {
                    message = "Username already exists"
                });
            }

            var newUser = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Username,
                NormalizedEmail = model.Email.ToUpperInvariant(),
                NormalizedUserName = model.Username.ToUpperInvariant()
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    message = "Successfully registered",
                    userId = newUser.Id,
                    username = newUser.UserName,
                    email = newUser.Email
                });
            }

            return BadRequest(new
            {
                message = "Registration failed",
                errors = result.Errors.Select(e => e.Description)
            });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSection = configuration.GetSection("Jwt");
            var key = jwtSection["Key"]!;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(
              int.Parse(jwtSection["ExpiresInMinutes"]!)
            );

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
