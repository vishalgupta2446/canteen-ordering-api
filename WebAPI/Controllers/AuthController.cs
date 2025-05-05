using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.Core.Security;
using TastyByThreeMusketeers.Infrastructure.Data;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [ApiController]
[Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly CanteenDbContext _dbContext;
        public AuthController(IJwtTokenService jwtTokenService, CanteenDbContext dbContext)
        {
            _jwtTokenService = jwtTokenService;
            _dbContext = dbContext;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest signupRequest)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Username == signupRequest.Username))
            {
                return BadRequest(new { message = "Username already exists." });
            }
            var user = new User
            {
                Username = signupRequest.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password),
                Role = signupRequest.Role,
                Email = signupRequest.Email
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok(new { message = "User registered successfully." });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }
            var token = _jwtTokenService.GenerateToken(user);
            return Ok(token);
        }
    }

    public class SignupRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }  // Optional: e.g., Admin, Customer
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
