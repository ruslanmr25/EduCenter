using Frontend.DTOs.StatisticsDTO;
using Frontend.Responses;
using Microsoft.AspNetCore.Components;

namespace Frontend.Clients;

public class CenterStatisticClient : BaseClient<CenterStatisticDto>
{
    public CenterStatisticClient(HttpClient client, NavigationManager navigationManager)
        : base(client, "api/center-admin/statistics", navigationManager) { }

    public async Task<Response<CenterStatisticDto>> GetStatistic()
    {
        try
        {
            var response = await client.GetAsync(Uri);

            return await HandleResponse<CenterStatisticDto>(response);
        }
        catch (Exception ex)
        {
            return HandleException<CenterStatisticDto>(ex);
        }
    }
}
