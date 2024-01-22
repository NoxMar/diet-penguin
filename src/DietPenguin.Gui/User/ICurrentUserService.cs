using DietPenguin.WebApi.Contracts.User;

namespace DietPenguin.Gui.User;

public interface ICurrentUserService
{
    Task FetchCurrentUser();

    Task<UserDto?> GetCurrentUserAsync();
}