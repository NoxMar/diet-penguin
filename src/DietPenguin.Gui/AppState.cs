using DietPenguin.WebApi.Contracts.User;

namespace DietPenguin.Gui;

public class AppState
{
    private UserDto? _user;

    public UserDto? User
    {
        get => _user;
        set
        {
            _user = value;
            NotifyStateChange();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChange() => OnChange?.Invoke();
}