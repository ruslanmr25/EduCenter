using System;
using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace UI.Clients;

public class GroupClient : BaseClient<Group>
{
    public GroupClient(HttpClient httpClient, NavigationManager navigationManager)
        : base(httpClient, "/api/Group", navigationManager) { }
}
