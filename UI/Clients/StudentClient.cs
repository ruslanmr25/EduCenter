using System;
using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace UI.Clients;

public class StudentClient : BaseClient<Student>
{
    public StudentClient(HttpClient httpClient, NavigationManager navigationManager)
        : base(httpClient, "/api/Student", navigationManager) { }
}
