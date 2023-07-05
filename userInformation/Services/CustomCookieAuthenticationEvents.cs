using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace userInformation.Services
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        
        

        private const string TicketIssuedTicks = nameof(TicketIssuedTicks);
        
        public override async Task SigningIn(CookieSigningInContext context)
        {
            context.Properties.SetString(TicketIssuedTicks, DateTimeOffset.UtcNow.Ticks.ToString());
            await base.SigningIn(context);
            
            //if (IsPersistent != true)
            //{

            //}
        }
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context )
        {
            var ticketIssuedTicksValue = context.Properties.GetString(TicketIssuedTicks);
           
            if (ticketIssuedTicksValue is null || !long.TryParse(ticketIssuedTicksValue , out var ticketIssuedTicks))
            {
                await RejectPrincipalAsync(context);
                return;
            }


            var ticketIssuedUtc = new DateTimeOffset(ticketIssuedTicks, TimeSpan.FromHours(0));  
            if (DateTimeOffset.UtcNow - ticketIssuedUtc > TimeSpan.FromMinutes(1))
            {
                await RejectPrincipalAsync(context);
                return;
            }

            await base.ValidatePrincipal(context);
        }

        private static async Task RejectPrincipalAsync( CookieValidatePrincipalContext context)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync();

        }

        //public static async Task RefreshLoginAsync(HttpContext context)
        //{
        //    try
        //    {

        //        if (context.User == null)
        //            return;


        //        var signInManager = context.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
        //        var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
        //        IdentityUser user = await userManager.GetUserAsync(context.User);


        //        if (signInManager.IsSignedIn(context.User))
        //        {
        //            await signInManager.RefreshSignInAsync(user);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //}
    }
}
