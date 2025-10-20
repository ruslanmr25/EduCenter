using System;
using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class GroupClient : BaseClient<Group>
{
    public GroupClient(HttpClient client, NavigationManager navigationManager)
        : base(client, "/api/Group", navigationManager) { }
}
