namespace UI.Responses;

public class Collection<T>
{
    public List<T> Items { get; set; } = new();

    public Meta? Meta { get; set; }
}

public class Meta
{
    public int TotalCount { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }
}
