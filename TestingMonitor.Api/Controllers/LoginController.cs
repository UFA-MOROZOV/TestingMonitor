namespace TestingMonitor.Api.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestingMonitor.Api.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    [HttpPost("/api/login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginCommand request)
    {
        if (request.Username != "admin" || request.Password != "123")
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "admin"),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = "TestingMonitor.Api",
            Audience = "TestingMonitor",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new LoginResponse
        {
            Token = tokenString,
            Expires = tokenDescriptor.Expires.Value,
            Username = "admin"
        });
    }
}