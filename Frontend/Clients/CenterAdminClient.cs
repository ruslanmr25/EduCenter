using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class CenterAdminClient : BaseClient<User>
{
    public CenterAdminClient(HttpClient client, NavigationManager navigation)
        : base(client, "/api/super-admin/users", navigation) { }
}
