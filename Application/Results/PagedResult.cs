using System;

namespace Application.Results;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public Meta Meta { get; set; }

    public PagedResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        Meta = new Meta(totalCount, page, pageSize);
    }
}

public class Meta
{
    public int TotalCount { get; set; }
    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public Meta(int totalCount, int page, int pageSize)
    {
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }
}
