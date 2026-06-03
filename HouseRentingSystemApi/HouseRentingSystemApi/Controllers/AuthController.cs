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
    [Produces(typeof(AuthResult))]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration configuration;

        private static readonly HashSet<string> AllowedRoles = new() { "Agent", "Client" };

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(PopulateResult(400, null, "Невалидни данни."));
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(PopulateResult(400, null, "Потребителят не съществува."));
            }

            var passwordOk = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordOk)
            {
                return Unauthorized(PopulateResult(400, null, "Грешна парола."));
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = await GenerateJwtToken(user, roles);

            Console.WriteLine(token);
            return Ok(PopulateResult(200, token, "Успешен вход."));
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(PopulateResult(400, null, "Невалидни данни."));
            }

            var role = model.Role ?? "Client";
            if (!AllowedRoles.Contains(role))
            {
                return BadRequest(PopulateResult(400, null, $"Невалидна роля '{role}'. Позволени: Agent, Client."));
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return Ok(PopulateResult(400, null, "Потребителят вече съществува."));
            }

            var newUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(PopulateResult(400, null, errors));
            }

            await _userManager.AddToRoleAsync(newUser, role);

            return Ok(PopulateResult(200, null, $"Потребителят е регистриран успешно с роля '{role}'."));
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user, IList<string> roles)
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

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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

        private AuthResult PopulateResult(int code, string? token = null, params string[] messages)
        {
            return new AuthResult
            {
                Code = code,
                Massage = string.Join(Environment.NewLine, messages),
                Token = token
            };
        }
    }
}
