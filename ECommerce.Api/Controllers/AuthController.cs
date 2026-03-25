using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Api.Controllers
{ // En enkel AuthController som hanterar inloggning och registrering av användare. Den använder en fejk-databas (en statisk lista) för att lagra användare så länge API:et är igång. 
  // I en riktig applikation skulle du naturligtvis använda en riktig databas och hashade lösenord!
    [Route("api/[controller]")] // API-endpointen kommer att vara /api/auth
    [ApiController]
    public class AuthController : ControllerBase
    { // Vi behöver IConfiguration för att läsa JWT-inställningarna från appsettings.json
        private readonly IConfiguration _config;

        // En fejk-databas med några fördefinierade användare. I en riktig applikation skulle du använda en riktig databas och hashade lösenord!
        private static readonly List<(string Email, string Password, string Role)> _users = new()
        {
            ("admin@test.com", "password123", "Admin"),
            ("user@test.com", "password123", "User")
        };

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Kollar om användaren finns i vår fejk-databas
            var user = _users.FirstOrDefault(u => u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase) 
                                               && u.Password == request.Password);

            if (user != default)
            {
                var token = GenerateJwtToken(user.Email, user.Role);
                return Ok(new AuthResponse { Token = token, Success = true });
            }

            return Unauthorized("Felaktig e-post eller lösenord.");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Kolla om e-posten redan är tagen
            if (_users.Any(u => u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("En användare med den e-postadressen finns redan.");
            }

            // Spara den nya användaren!
            _users.Add((request.Email, request.Password, "User"));

            return Ok(new AuthResponse { Success = true });
        }

        private string GenerateJwtToken(string email, string role)
        { // Skapar en JWT-token med e-post och roll som claims, och signerar den med en hemlig nyckel från appsettings.json
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}