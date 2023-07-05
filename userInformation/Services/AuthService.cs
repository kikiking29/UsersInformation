using userInformation.Interfaces;
using userInformation.Models;
using userInformation.ConnecDb;
using userInformation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace userInformation.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenService tokenService;

        private readonly IHttpContextAccessor httpContextAccessor;
        connecDb conn = new connecDb();
        public AuthService( ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            this.tokenService = tokenService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            PasswordModels pass= new PasswordModels();
            PasswordModels  passwrd = new PasswordModels();
            
            UserLoginResponse response = new();

            pass.username = request.Username;
            pass.old_password = request.Password;
            passwrd = conn.ChackPassword(pass);
           

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request));
                
            }
            
            if (request.Username == passwrd.username && request.Password == passwrd.old_password)
            {
                GenerateTokenRequest genTokenRequest = new GenerateTokenRequest();
                genTokenRequest.Username = request.Username;
                Task<GenerateTokenResponse> genTokenresponse = tokenService.GenerateToken(genTokenRequest);

                response.AccessTokenExpireDate = genTokenresponse.Result.TokenExpireDate;
                response.AuthenticateResult = true;
                response.AuthToken = genTokenresponse.Result.Token;

            }
            return Task.FromResult(response);
        }
        
    }
}
