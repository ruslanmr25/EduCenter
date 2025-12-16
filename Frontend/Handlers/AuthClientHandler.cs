using Frontend.Services;

namespace Frontend.Handlers;

public class AuthClientHandler : DelegatingHandler
{
    private TokenService _tokenService;

    public AuthClientHandler(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var token = await _tokenService.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
