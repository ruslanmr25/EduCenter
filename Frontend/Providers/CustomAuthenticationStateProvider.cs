using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Providers;

# warning Bu yerga refresh token qo'sh
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly TokenService _tokenService;

    public CustomAuthenticationStateProvider(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await _tokenService.GetTokenAsync();

        if (string.IsNullOrEmpty(token))
            return new AuthenticationState(_anonymous);

        var claims = GetClaims(token);

        if (claims is null)
        {
            return new AuthenticationState(_anonymous);
        }
        var identity = new ClaimsIdentity(claims, "jwt");

        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    private List<Claim>? GetClaims(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = handler.ReadJwtToken(jwtToken);

        if (token.ValidTo < DateTime.UtcNow)
        {
            return null;
        }

        var id = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var name = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

        if (id == null || name == null || role == null)
        {
            return null;
        }

        var claims = new List<Claim> { id, name, role };
        return claims;
    }

    public Task NotifyUserAuthentication(string token)
    {
        var claims = GetClaims(token);

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

        return Task.CompletedTask;
    }

    public void NotifyUserLogout()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}
