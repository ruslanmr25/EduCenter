using Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace UI.Clients;

public class ScienceClient : BaseClient<Science>
{
    public ScienceClient(HttpClient httpClient, NavigationManager navigationManager)
        : base(httpClient, "/api/Science", navigationManager) { }
}
