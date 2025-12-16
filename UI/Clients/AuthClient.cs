using System.Net.Http.Json;
using System.Text.Json;
using UI.DTOs;
using UI.Providers;
using UI.Responses;

namespace UI.Clients;

public class AuthClient
{
    private readonly TokenProvider _tokenProvider;

    private readonly HttpClient _httpClient;

    private readonly string loginUri = "/api/Auth/login";

    public AuthClient(TokenProvider tokenProvider, HttpClient httpClient)
    {
        this._tokenProvider = tokenProvider;
        this._httpClient = httpClient;
    }

    public async Task<Response<LoginResponse>> Login(LoginDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(loginUri, dto);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return new Response<LoginResponse>()
                {
                    Success = false,

                    Message = "Serverda xatolik keyinroq urinib ko'ring",
                };
            }

            var result = await response.Content.ReadFromJsonAsync<Response<LoginResponse>>();

            return result!;
        }
        catch (HttpRequestException ex)
        {
            return new Response<LoginResponse>
            {
                Success = false,
                Message = $"Serverga ulanib bo'lmadi: {ex.Message}",
            };
        }
        catch (TaskCanceledException)
        {
            return new Response<LoginResponse>
            {
                Success = false,
                Message = "Iltimos keyinroq urinib ko'ring",
            };
        }
        catch (JsonException)
        {
            return new Response<LoginResponse>
            {
                Success = false,
                Message = "So'rovda nimadir xato ketti",
            };
        }
        catch (Exception ex)
        {
            return new Response<LoginResponse>
            {
                Success = false,
                Message = $"Kutilmagan xato: {ex.Message}",
            };
        }
    }
}
