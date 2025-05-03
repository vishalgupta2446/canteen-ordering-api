using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Core.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, string role);
    }
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _configuration;
        public JwtTokenService(IOptions<JwtSettings> configuration)
        {
            _configuration = configuration.Value;
        }
        public string GenerateToken(string username, string role)
        {
            var claims = new[]
            {
           new Claim(ClaimTypes.Name, username),
           new Claim(ClaimTypes.Role, role)
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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
