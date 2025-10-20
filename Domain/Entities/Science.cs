using System;

namespace Domain.Entities;

public class Science : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Center Center { get; set; }

    public int CenterId { get; set; }
}
