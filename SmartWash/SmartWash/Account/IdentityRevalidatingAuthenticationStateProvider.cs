using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SmartWash.Domain.Entities;

namespace SmartWash.WebUI.Account
{
    // This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
    // every 30 minutes an interactive circuit is connected.
    public class IdentityRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly PersistentComponentState _state;
        private readonly IdentityOptions _options;

        private readonly PersistingComponentStateSubscription _stateSubscription;
        ILoggerFactory _loggerFactory;

        private Task<AuthenticationState>? _authenticationStateTask;

        public IdentityRevalidatingAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            IOptions<IdentityOptions> options,
            PersistentComponentState state) : base(loggerFactory)
        {

                _scopeFactory = scopeFactory;
            _state = state;
            _options = options.Value;
            AuthenticationStateChanged += OnAuthenticationStateChanged;
            _stateSubscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
            }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            // Get the user manager from a new scope to ensure it fetches fresh data
            await using var scope = _scopeFactory.CreateAsyncScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            return await ValidateSecurityStampAsync(userManager, authenticationState.User);
        }

        private async Task<bool> ValidateSecurityStampAsync(UserManager<ApplicationUser> userManager, ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user is null)
            {
                return false;
            }
            else if (!userManager.SupportsUserSecurityStamp)
            {
                return true;
            }
            else
            {
                var principalStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType);
                var userStamp = await userManager.GetSecurityStampAsync(user);
                return principalStamp == userStamp;
            }
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
        {
            _authenticationStateTask = authenticationStateTask;
        }

        private async Task OnPersistingAsync()
        {
            if (_authenticationStateTask is null)
            {
                throw new UnreachableException($"Authentication state not set in {nameof(RevalidatingServerAuthenticationStateProvider)}");
            }

            var authenticationState = await _authenticationStateTask;
            var principal = authenticationState.User;

            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(_options.ClaimsIdentity.UserIdClaimType)?.Value;
                var email = principal.FindFirst(_options.ClaimsIdentity.EmailClaimType)?.Value;
                var role = principal.FindFirst(_options.ClaimsIdentity.RoleClaimType)?.Value;

                if (userId != null && email != null)
                {
                    _state.PersistAsJson(nameof(UserInfo), new UserInfo
                    { 
                      UserID = userId,
                      Email = email,
                      Role = role
                    });
                }
            }

        }

        protected override void Dispose(bool disposing)
        {
            _stateSubscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
            base.Dispose(disposing);
        }

    }
}
