using Domain.Enums;
using Newtonsoft.Json;

namespace Domain.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    [JsonIgnore]
    public string Password { get; set; } = string.Empty;

    public Role Role { get; set; }

    public List<Center> Centers { get; set; } = [];

    public Center? Center { get; set; }
}
