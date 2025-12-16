namespace Api.Responses;

public class ApiResponse<T>(
    T? item = default,
    bool success = true,
    string message = "Operatsiya muvaffaqiyatli amalga oshirildi"
)
{
    public bool Success { get; set; } = success;

    public string Message { get; set; } = message;

    public T? Data { get; set; } = item;

    public List<string> Errors = [];
}
