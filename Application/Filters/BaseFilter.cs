using System;

namespace Application.Filters;

public class BaseFilter
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
