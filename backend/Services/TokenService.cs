using backend.JwtUtil;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Services
{
    public class TokenService : ITokenService
    {
        readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            var key = Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]);
            var dateTimeNow = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new Claim("userId", request.UserId.ToString()),
                new Claim("userName", request.Username)
            };

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: dateTimeNow,
                expires: dateTimeNow.AddHours(8),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return Task.FromResult(new GenerateTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDate = dateTimeNow.AddHours(8)
            });
        }
    }
}
