using userInformation.Interfaces;
using userInformation.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace userInformation.Services
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
            GenerateTokenResponse tokenResponse = new GenerateTokenResponse();
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]));

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: new List<Claim> { new Claim("username", request.Username)},
                    notBefore: dateTimeNow,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(500)),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );
            tokenResponse = new GenerateTokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(500))
            };

            return Task.FromResult(tokenResponse);
        }
    }
}
