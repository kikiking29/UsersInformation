using userInformation.Interfaces;
using userInformation.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore;

namespace userInformation.Services
{
    public class TokenService : ITokenService
    {
        readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            GenerateTokenResponse tokenResponse = new GenerateTokenResponse();
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]));

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: new List<Claim> { 
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim("session", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Email, request.Username)},
                    notBefore: dateTimeNow,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(1)),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                ) ;

            var claimsIdentity = new ClaimsIdentity(jwt.Claims, "MyAuthScheme");
            httpContextAccessor.HttpContext.SignInAsync("MyAuthScheme", new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());
            //httpContextAccessor.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
            //RefreshLoginAsync();

            tokenResponse = new GenerateTokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(1))
            };

            return Task.FromResult(tokenResponse);
        }

        public async Task RefreshLoginAsync()
        {
            try
            {


                if (httpContextAccessor.HttpContext == null)
                    return ;


                
                var userManager = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                var signInManager = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
                IdentityUser user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);


                if (signInManager.IsSignedIn(httpContextAccessor.HttpContext.User))
                {
                    await signInManager.RefreshSignInAsync(user);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

    }
}
