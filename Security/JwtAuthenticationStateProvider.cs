using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace POSProject.Security;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorage;

    public JwtAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetItemAsync<string>("token");
        var principal = TokenManager.GetPrincipal(token);
        return principal == null || !IsActualToken(principal)
            ? new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))
            : new AuthenticationState(principal);
    }

    public void NotifyStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private static bool IsActualToken(ClaimsPrincipal principal)
    {
        return DateTime.Parse(principal.FindFirst(x => x.Type == ClaimTypes.Expired)?.Value ??
                              DateTime.MinValue.ToString("s")) > DateTime.Now;
    }
}