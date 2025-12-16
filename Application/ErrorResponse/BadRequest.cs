namespace Application.ErrorResponse;

public class BadRequest
{
    public BadRequest() { }

    public BadRequest(List<object> errors, string message = "So‘rovda xatolik bor")
    {
        Errors = errors;

        Message = message;
    }

    public bool Success { get; set; } = false;

    public object? Data { get; set; } = null;

    public string Message { get; set; } = "So‘rovda xatolik bor";

    public IEnumerable<object>? Errors { get; set; }
}
