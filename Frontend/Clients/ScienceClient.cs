using System;
using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class ScienceClient : BaseClient<Science>
{
    public ScienceClient(HttpClient client, NavigationManager navigationManager)
        : base(client, "/api/Science", navigationManager) { }
}
