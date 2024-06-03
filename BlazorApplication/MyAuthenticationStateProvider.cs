using BlazorApplication.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public MyAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var user = await GetUserFromLocalStorage();
            if (user.data.token == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            var claims = new[]
         {
            new Claim(ClaimTypes.Authentication, user.data.token),
    };
            var identity = user != null ? new ClaimsIdentity(claims, "jwt") : new ClaimsIdentity();

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return new AuthenticationState(claimsPrincipal);
        }
        catch (Exception ex) { return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); }
    }

    private async Task<LoginRoot> GetUserFromLocalStorage()
    {
        if (await _localStorage.ContainKeyAsync("user"))
        {
            var userData = await _localStorage.GetItemAsync<LoginRoot>("user");
            if (userData != null)
            {
                var claims = userData;
                return claims;
            }
        }
        return null;
    }

    public async Task MarkUserAsAuthenticated()
    {
        var userlc = await GetUserFromLocalStorage();
        var claims = new[]
    {
            new Claim(ClaimTypes.Authentication, userlc.data.token),
    };

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        // Remove user data from local storage
        await _localStorage.RemoveItemAsync("user");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }
}
