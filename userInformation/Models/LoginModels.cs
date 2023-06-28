namespace userInformation.Models
{
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginResponse
    {
        public bool AuthenticateResult { get; set; }
        public string AuthToken { get; set; }
        
        public DateTime AccessTokenExpireDate { get; set; }
        //public GenerateTokenResponse Token { get; set; }
    }
    public class GenerateTokenRequest
    {
        public string Username { get; set; }
    }
    public class GenerateTokenResponse
    {
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
