using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using UI.Extensions;
using UI.Handlers;
using UI.Responses;

namespace UI.Clients;

public class BaseClient<T>
    where T : class
{
    protected readonly HttpClient httpClient;

    protected readonly string Uri;

    protected readonly NavigationManager navigationManager;

    public BaseClient(HttpClient httpClient, string uri, NavigationManager navigationManager)
    {
        this.httpClient = httpClient;
        Uri = uri;
        this.navigationManager = navigationManager;
    }

    public async Task<Response<Collection<T>>> GetAllAsync()
    {
        try
        {
            var response = await httpClient.GetAsync(Uri);
            return await ApiResponseHandler.HandleResponse<Collection<T>>(
                response,
                navigationManager
            );
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<Collection<T>>(ex);
        }
    }

    public async Task<Response<Collection<T>>> GetAllAsync(object query)
    {
        try
        {
            var response = await httpClient.GetAsync(Uri + $"?{query.ToQueryString()}");
            return await ApiResponseHandler.HandleResponse<Collection<T>>(
                response,
                navigationManager
            );
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<Collection<T>>(ex);
        }
    }

    public async Task<Response<Collection<T>>> GetAllAsync(int page = 1)
    {
        try
        {
            var response = await httpClient.GetAsync($"{Uri}?page={page}&pageSize=50");
            return await ApiResponseHandler.HandleResponse<Collection<T>>(
                response,
                navigationManager
            );
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<Collection<T>>(ex);
        }
    }

    public async Task<Response<T>> GetAsync(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"{Uri}/{id}");
            return await ApiResponseHandler.HandleResponse<T>(response, navigationManager);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<T>(ex);
        }
    }

    public async Task<Response<T>> PostAsync(string url, object data)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(url, data);
            return await ApiResponseHandler.HandleResponse<T>(response, navigationManager);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<T>(ex);
        }
    }

    public async Task<Response<T>> PostAsync(object data) => await PostAsync(Uri, data);

    public async Task<Response<T>> PutAsync(int id, object data)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{Uri}/{id}", data);
            return await ApiResponseHandler.HandleResponse<T>(response, navigationManager);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<T>(ex);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"{Uri}/{id}");
            return await ApiResponseHandler.HandleResponse<bool>(response, navigationManager);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException<bool>(ex);
        }
    }
}
