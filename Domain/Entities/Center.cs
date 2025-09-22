using System;

namespace Domain.Entities;

public class Center
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public User CenterAdmin { get; set; }

    public int CenterAdminId { get; set; }

    public List<User> Teachers { get; set; }
}
