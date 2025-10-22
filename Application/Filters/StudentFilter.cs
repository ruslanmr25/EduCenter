using System;

namespace Application.Filters;

public class StudentFilter : BaseFilter
{
    public string? FullName { get; set; }

    public int? GroupId { get; set; }
}
