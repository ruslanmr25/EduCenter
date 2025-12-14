using System;
using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace UI.Clients;

public class TeacherClient : BaseClient<User>
{
    public TeacherClient(HttpClient httpClient, NavigationManager navigationManager)
        : base(httpClient, "/api/Teacher", navigationManager) { }
}
