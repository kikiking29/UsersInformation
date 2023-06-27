using userInformation.Interfaces;
using userInformation.Models;
using userInformation.ConnecDb;
using userInformation.Services;

namespace userInformation.Services
{
    public class AuthService : IAuthService
    {
        connecDb conn = new connecDb();
        
        
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
                response.AccessTokenExpireDate = DateTime.UtcNow;
                response.AuthenticateResult = true;
                response.AuthToken = string.Empty;
                
            }
            return Task.FromResult(response);
        }



    }
}
