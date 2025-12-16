using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using UI.Responses;

namespace UI.Handlers;

public static class ApiResponseHandler
{
    public static async Task<Response<TResponse>> HandleResponse<TResponse>(
        HttpResponseMessage response,
        NavigationManager navigationManager
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
}
