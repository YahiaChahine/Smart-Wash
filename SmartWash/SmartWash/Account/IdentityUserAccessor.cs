using Microsoft.AspNetCore.Identity;
using SmartWash.Domain.Entities;

namespace SmartWash.WebUI.Account
{
    internal sealed class IdentityUserAccessor(
        IHttpContextAccessor httpContextAccessor
        ,UserManager<ApplicationUser> userManager,
        IdentityRedirectManager redirectManager)
    {
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            var principal = httpContextAccessor.HttpContext?.User ??
                throw new InvalidOperationException($"{nameof(GetRequiredUserAsync)} requires access");

            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
