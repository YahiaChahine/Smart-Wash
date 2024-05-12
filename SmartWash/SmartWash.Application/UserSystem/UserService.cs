using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SmartWash.Domain.Entities;

namespace SmartWash.Application.UserSystem
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider _auth;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(AuthenticationStateProvider auth, UserManager<ApplicationUser> userManager)
        {
            _auth = auth;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUser()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            return await _userManager.GetUserAsync(authState.User);
        }

        public async Task<string> GetUserId()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = await _userManager.GetUserAsync(authState.User);

            if (user is null)
                throw new InvalidOperationException("User not found");

            return user.Id;
        }

        public async Task<string> GetUserName()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = await _userManager.GetUserAsync(authState.User);

            if (user is null)
                throw new InvalidOperationException("User not found");

            return user.UserName;
        }

        public async Task<string> GetUserEmail()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = await _userManager.GetUserAsync(authState.User);

            if (user is null)
                throw new InvalidOperationException("User not found");

            return user.Email;
        }

        public async Task<string> GetUserRole()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = await _userManager.GetUserAsync(authState.User);

            if (user is null)
                throw new InvalidOperationException("User not found");

            throw new NotImplementedException(); // TODO: Return user role
        }
    }
}
