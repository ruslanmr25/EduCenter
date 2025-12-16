using Domain.Entities;
using Frontend.Responses;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Frontend.Clients;

public class GroupClient : BaseClient<Group>
{
    public GroupClient(HttpClient client, NavigationManager navigationManager)
        : base(client, "/api/Group", navigationManager) { }

    public async Task<Response<Group>> UpdateActiveStateAsync(int id, bool isActive)
    {
        try
        {
            var response = await client.PutAsJsonAsync(
                $"{Uri}/{id}/change-status",
                new { IsActive = isActive }
            );
            return await HandleResponse<Group>(response);
        }
        catch (Exception ex)
        {
            return HandleException<Group>(ex);
        }
    }
}
