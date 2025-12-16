using System.Net;

namespace UI.Responses;

public class Response<T>
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; } = default;

    public List<BadRequestErrors> Errors { get; set; } = new();
}
