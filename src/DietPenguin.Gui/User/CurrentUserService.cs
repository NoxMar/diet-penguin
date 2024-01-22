using DietPenguin.Gui.Api;
using DietPenguin.WebApi.Contracts.User;

namespace DietPenguin.Gui.User;

public class CurrentUserService : ICurrentUserService
{
    private IBackendApiClient _apiClient;
    private AppState _appState;

    public CurrentUserService(IBackendApiClient apiClient, AppState appState)
    {
        _apiClient = apiClient;
        _appState = appState;
    }

    public async Task FetchCurrentUser()
    {
        var result = await _apiClient.GetCurrentUser();
        if (result.IsSuccess)
        {
            _appState.User = result.Value;
        }
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        if (_appState.User is null)
        {
            await FetchCurrentUser();
        }

        return _appState.User;
    }
}