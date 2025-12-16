using Blazored.LocalStorage;

namespace Frontend.Services;

public class TokenService
{
    private const string TokenKey = "authToken";
    private readonly ILocalStorageService _localStorage;

    public TokenService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task SetTokenAsync(string token) =>
        await _localStorage.SetItemAsync(TokenKey, token);

    public async Task<string?> GetTokenAsync() =>
        await _localStorage.GetItemAsync<string>(TokenKey);

    public async Task RemoveTokenAsync() => await _localStorage.RemoveItemAsync(TokenKey);
}
