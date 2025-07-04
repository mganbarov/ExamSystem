using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Application.DTOs.Tokens;
using ExamSystem.Infrastructure.Configurations;

namespace ExamSystem.Infrastructure.Implementations
{
    public class TokenHandler : ITokenHandler
    {
        private readonly JwtSettings _jwtSettings;
        public TokenHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public AuthTokenDto CreateJwt(string userName)
        {
            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthTokenDto(tokenString, expiration);

        }
    }
}
