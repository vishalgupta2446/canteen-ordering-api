using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TastyByThreeMusketeers.Core.Security;

namespace TastyByThreeMusketeers.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Validate the user (this can be connected to a user store/database)
            if (loginRequest.Username == "admin" && loginRequest.Password == "adminpassword")
            {
                var token = _jwtTokenService.GenerateToken(loginRequest.Username, "Admin");
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
    
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
