using System;
using UI.Providers;

namespace UI.Handlers;

public class MessageHandler : DelegatingHandler
{
    private readonly TokenProvider _tokenProvider;

    public MessageHandler(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var token = await _tokenProvider.GetToken();

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
