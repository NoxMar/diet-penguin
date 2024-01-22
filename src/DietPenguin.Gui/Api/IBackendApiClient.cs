using DietPenguin.Core;
using DietPenguin.WebApi.Contracts.User;

namespace DietPenguin.Gui.Api;

public interface IBackendApiClient
{
    public Task<Result<UserDto>> GetCurrentUser();
}