﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using SmartWash.Domain.Entities;

namespace SmartWash.WebUI
{
    public class IdentityAuthenticationStateProvider(PersistentComponentState persistentState) : AuthenticationStateProvider
    {
        private static readonly Task<AuthenticationState> _unauthenticatedTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!persistentState.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
            {
                return _unauthenticatedTask;
            }

            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier, userInfo.UserID),
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.Role)
            ];

            return Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims,
                authenticationType: nameof(IdentityAuthenticationStateProvider)))));

        }
    }
}
