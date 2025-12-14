using System;
using System.Net.Http.Json;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using UI.DTOs;
using UI.Handlers;
using UI.Responses;

namespace UI.Clients;

public class StudentPaymentSystemClient
{
    private readonly HttpClient _httpClient;

    protected readonly NavigationManager navigationManager;

    public StudentPaymentSystemClient(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        this.navigationManager = navigationManager;
    }

    private readonly string uri = "/api/StudentPayment";

    public async Task<Response<List<GroupStudentPaymentSycle>>> GetPendingFeeStudents()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{uri}/pending-fees");

            return await ApiResponseHandler.HandleResponse<List<GroupStudentPaymentSycle>>(
                response,
                navigationManager
            );
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<List<GroupStudentPaymentSycle>>(ex);
        }
    }

    public async Task<Response<object>> Pay(CreateStudentPaymentDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{uri}/pay", dto);

            return await ApiResponseHandler.HandleResponse<object>(response, navigationManager);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<object>(ex);
        }
    }
}
