using Domain.Entities;
using Frontend.DTOs.Payments;
using Frontend.Responses;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend.Clients
{
    public class StudentPaymentClient
    {
        protected readonly HttpClient client;
        protected readonly NavigationManager navigationManager;
        public string Uri;

        public StudentPaymentClient(HttpClient client, NavigationManager navigationManager)
        {
            this.client = client;
            Uri = "/api/StudentPayment";
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
                    Message = "Serverda xatolik, iltimos keyinroq urinib ko'ring!",
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

        public async Task<Response<Student>> GetSycles(int studentId)
        {
            try
            {
                var response = await client.GetAsync($"{Uri}/students/{studentId}");
                return await HandleResponse<Student>(response);
            }
            catch (Exception ex)
            {
                return HandleException<Student>(ex);
            }
        }

        public async Task<Response<Group>> GetGroupSycle(int groupId)
        {
            try
            {
                var response = await client.GetAsync($"{Uri}/groups/{groupId}");
                return await HandleResponse<Group>(response);
            }
            catch (Exception ex)
            {
                return HandleException<Group>(ex);
            }
        }

        public async Task<Response<List<Student>>> GetPendingFees()
        {
            try
            {
                var response = await client.GetAsync($"{Uri}/students/pending-fees");
                return await HandleResponse<List<Student>>(response);
            }
            catch (Exception ex)
            {
                return HandleException<List<Student>>(ex);
            }
        }

        public async Task<Response<object>> PayAsync(
            int studentId,
            List<StudentPaymentDto> payments
        )
        {
            try
            {
                var response = await client.PostAsJsonAsync(
                    $"{Uri}/students/{studentId}/pay",
                    payments
                );
                return await HandleResponse<object>(response);
            }
            catch (Exception ex)
            {
                return HandleException<object>(ex);
            }
        }
    }
}
