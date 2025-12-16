using Blazored.LocalStorage;

namespace UI.Providers;

public class TokenProvider
{
    private readonly ILocalStorageService _storage;

    public TokenProvider(ILocalStorageService storage)
    {
        _storage = storage;
    }

    public async Task SetToken(string token) => await _storage.SetItemAsync("token", token);

    public async Task<string?> GetToken() => await _storage.GetItemAsync<string>("token");
}
