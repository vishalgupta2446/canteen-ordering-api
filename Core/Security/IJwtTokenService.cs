using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TastyByThreeMusketeers.Core.Models;
using TastyByThreeMusketeers.WebAPI.DTOs;

namespace TastyByThreeMusketeers.Core.Security
{
    public interface IJwtTokenService
    {
        AuthResponseDTO GenerateToken(User user);
    }
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _configuration;
        public JwtTokenService(IOptions<JwtSettings> configuration)
        {
            _configuration = configuration.Value;
        }
        public AuthResponseDTO GenerateToken(User user)
        {
            var claims = new[]
            {
           new Claim(ClaimTypes.Name, user.Username),
           new Claim(ClaimTypes.Role, user.Role),
           new Claim("UserId", user.Id.ToString())  // Adding UserId claim if needed
       };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.Role,
                Username = user.Username,
                UserId = user.Id
            };
        }
    }
}
