using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using UI.Handlers;
using UI.Responses;

namespace UI.Clients;

public class GroupClient : BaseClient<Group>
{
    public GroupClient(HttpClient httpClient, NavigationManager navigationManager)
        : base(httpClient, "/api/Group", navigationManager) { }

    public async Task<Response<List<StudentPaymentRowModel>>> GetGroupSycleModel(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{Uri}/{id}/sycle");
            return await ApiResponseHandler.HandleResponse<List<StudentPaymentRowModel>>(
                response,
                navigationManager
            );
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<List<StudentPaymentRowModel>>(ex);
        }
    }
}
