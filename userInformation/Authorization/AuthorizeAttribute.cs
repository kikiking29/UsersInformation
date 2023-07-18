using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using userInformation.ConnecDb;
using userInformation.Entities;
using userInformation.Controllers;
using Microsoft.AspNetCore.Authentication;
using Org.BouncyCastle.Ocsp;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Controllers;
using MySqlX.XDevAPI.Common;

namespace userInformation.Authorization
{
   

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
           
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization


            //var user = (User)context.HttpContext.Items["User"];
            var data = context.HttpContext.Features.Get<User>();

            if (data == null || (_roles.Any() && !_roles.Contains(data.Role)))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
