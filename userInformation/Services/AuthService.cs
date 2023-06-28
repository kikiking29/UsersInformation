using userInformation.Interfaces;
using userInformation.Models;
using userInformation.ConnecDb;
using userInformation.Services;

namespace userInformation.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenService tokenService;
        connecDb conn = new connecDb();
        public AuthService( ITokenService tokenService)
        {
            this.tokenService = tokenService;
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
                //Task.FromResult(genTokenresponse);
                //Task.FromResult(tokenService.GenerateToken(genTokenRequest));
            }
            return Task.FromResult(response);
        }



    }
}
