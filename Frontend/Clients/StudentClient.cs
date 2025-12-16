using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class StudentClient : BaseClient<Student>
{
    public StudentClient(HttpClient client, NavigationManager navigationManager)
        : base(client, "/api/Student", navigationManager) { }
}
