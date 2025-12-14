using System;
using UI.Responses;

namespace UI.Handlers;

public class ApiExceptionHandler
{
    public static Response<TResponse> HandleException<TResponse>(Exception ex)
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
}
