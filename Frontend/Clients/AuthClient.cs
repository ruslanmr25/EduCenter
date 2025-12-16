using Frontend.DTOs.Auth;
using Frontend.Responses;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend.Clients;

public class AuthClient
{
    private readonly HttpClient _client;

    private string loginUri = "/api/Auth/login";

    public AuthClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<LoginResponse>> LoginAsync(LoginDto dto)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(loginUri, dto);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
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
                Message = "Invalid response format",
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
