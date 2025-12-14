using System;

namespace Common.Queries;

public class StudentQuery : BaseQuery
{
    public string? FullName { get; set; }

    public int? ScienceId { get; set; }

    public int? GroupId { get; set; }
}
