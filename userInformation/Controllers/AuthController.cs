using userInformation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using userInformation.ConnecDb;
using userInformation.Models;
using userInformation.Services;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;

namespace userInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController
    {
        readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        
  
        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserLoginResponse>> LoginUserAsync([FromBody] UserLoginRequest request)
        {
            var result = await authService.LoginUserAsync(request);

            return result;
        }

    }
}
