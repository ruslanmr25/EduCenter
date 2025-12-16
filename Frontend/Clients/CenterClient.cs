using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class CenterClient : BaseClient<Center>
{
    public CenterClient(HttpClient client, NavigationManager navigationManager)
        : base(client, "/api/Center", navigationManager) { }
}
