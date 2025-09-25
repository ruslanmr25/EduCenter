using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Frontend.Responses;

namespace Frontend.Clients;

public class BaseClient
{
    protected readonly HttpClient client;

    public BaseClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Response<T>> PostAsync<T>(string url, object data)
    {
        try
        {
            var response = await client.PostAsJsonAsync(url, data);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new Response<T>
                {
                    Success = false,
                    Message = "Serverda xatolik iltimos keyinroq urinib ko'ring!",
                    Errors = [],
                };
            }

            var result = await response.Content.ReadFromJsonAsync<Response<T>>();
            return result ?? new Response<T> { Success = false, Message = "Empty response" };
        }
        catch (HttpRequestException ex)
        {
            return new Response<T>
            {
                Success = false,
                Message = $"Serverga ulanib bo'lmadi: {ex.Message}",
            };
        }
        catch (TaskCanceledException)
        {
            return new Response<T> { Success = false, Message = "Iltimos keyinroq urinib ko'ring" };
        }
        catch (JsonException)
        {
            return new Response<T> { Success = false, Message = "Invalid response format" };
        }
        catch (Exception ex)
        {
            return new Response<T> { Success = false, Message = $"Kutilmagan xato: {ex.Message}" };
        }
    }
}
