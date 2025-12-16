namespace UI.Responses;

public class BadRequestErrors
{
    public string Field { get; set; } = string.Empty;

    public List<string> Errors { get; set; } = new();
}
