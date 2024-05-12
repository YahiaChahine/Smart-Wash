using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace SmartWash.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly AuthenticationStateProvider _auth;

        public UserRepository(AuthenticationStateProvider auth)
        {
            _auth = auth;
        }

        public async Task<string> GetUserId()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(c => c.Type == "sub").Value;
        }

        public async Task<string> GetUserName()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity.Name;
        }

        public async Task<string> GetUserEmail()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(c => c.Type == "email").Value;
        }

        public async Task<string> GetUserRole()
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(c => c.Type == "role").Value;
        }
    }
}
