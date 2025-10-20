using System;
using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class TeacherClient : BaseClient<User>
{
    public TeacherClient(HttpClient client, NavigationManager navigationManager)
        : base(client, "/api/Teacher", navigationManager) { }
}
