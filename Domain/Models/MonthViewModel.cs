namespace Domain.Models;

public class MonthViewModel<TModel>
{
    public string MonthName { get; set; } = string.Empty;
    public List<TModel> Rows { get; set; } = new List<TModel>();
}
