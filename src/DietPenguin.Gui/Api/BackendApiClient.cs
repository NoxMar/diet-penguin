using System.Net.Http.Json;
using DietPenguin.Core;
using DietPenguin.WebApi.Contracts;
using DietPenguin.WebApi.Contracts.User;
using Microsoft.JSInterop;

namespace DietPenguin.Gui.Api;

public class BackendApiClient : IBackendApiClient
{
    private readonly HttpClient _httpClient;

    public BackendApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<UserDto>> GetCurrentUser()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<UserDto>(GetUserFull.RouteTemplate);
            return response is not null ? Result<UserDto>.Success(response) 
                : Result<UserDto>.Failure(ApiErrors.DeserializationFailed);
        }
        catch (HttpRequestException _)
        {
            return Result<UserDto>.Failure(ApiErrors.NotSuccessStatusCode);
        }
        catch (JSException _)
        {
            return Result<UserDto>.Failure(ApiErrors.DeserializationFailed);
        }
    }
}