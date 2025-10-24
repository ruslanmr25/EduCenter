using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Frontend.Responses;
using Frontend.Services;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class BaseClient<T>
{
    protected readonly HttpClient client;

    protected readonly NavigationManager navigationManager;

    public string Uri;

    public BaseClient(HttpClient client, string uri, NavigationManager navigationManager)
    {
        this.client = client;
        Uri = uri;
        this.navigationManager = navigationManager;
    }

    protected async Task<Response<TResponse>> HandleResponse<TResponse>(
        HttpResponseMessage response
    )
    {
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            return new Response<TResponse>
            {
                HttpStatusCode = response.StatusCode,
                Success = false,
                Message = "Serverda xatolik iltimos keyinroq urinib ko'ring!",
                Errors = [],
            };
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            navigationManager.NavigateTo("/login");
            return new Response<TResponse>() { Success = false };
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            navigationManager.NavigateTo("/forbidden");
            return new Response<TResponse>() { Success = false };
        }
        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var result = await response.Content.ReadFromJsonAsync<Response<TResponse>>(options);

            if (result is not null)
            {
                result.HttpStatusCode = response.StatusCode;
                result.Success = true;
            }

            return result
                ?? new Response<TResponse> { Success = false, Message = "Empty response" };
        }
        catch (JsonException)
        {
            return new Response<TResponse>
            {
                Success = false,
                Message = "Invalid response format",
                HttpStatusCode = response.StatusCode,
            };
        }
    }

    protected static Response<TResponse> HandleException<TResponse>(Exception ex)
    {
        return ex switch
        {
            HttpRequestException h => new Response<TResponse>
            {
                Success = false,
                Message = $"Serverga ulanib bo'lmadi: {h.Message}",
            },
            TaskCanceledException => new Response<TResponse>
            {
                Success = false,
                Message = "Iltimos keyinroq urinib ko'ring",
            },
            _ => new Response<TResponse>
            {
                Success = false,
                Message = $"Kutilmagan xato: {ex.Message}",
            },
        };
    }

    // --- CRUD metodlari ---

    public async Task<Response<Collection<T>>> GetAllAsync(int page = 1, int pageSize = 100)
    {
        try
        {
            var response = await client.GetAsync($"{Uri}?page={page}&pageSize={pageSize}");
            return await HandleResponse<Collection<T>>(response);
        }
        catch (Exception ex)
        {
            return HandleException<Collection<T>>(ex);
        }
    }

    public async Task<Response<T>> GetAsync(int id)
    {
        try
        {
            var response = await client.GetAsync($"{Uri}/{id}");
            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }

    public async Task<Response<T>> PostAsync(string url, object data)
    {
        try
        {
            var response = await client.PostAsJsonAsync(url, data);
            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }

    public async Task<Response<T>> PostAsync(object data) => await PostAsync(Uri, data);

    public async Task<Response<T>> PutAsync(int id, object data)
    {
        try
        {
            var response = await client.PutAsJsonAsync($"{Uri}/{id}", data);
            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"{Uri}/{id}");
            return await HandleResponse<bool>(response);
        }
        catch (Exception ex)
        {
            return HandleException<bool>(ex);
        }
    }
}
