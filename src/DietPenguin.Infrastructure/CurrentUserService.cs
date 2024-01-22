using DietPenguin.Domain.User;

namespace DietPenguin.Infrastructure;

public class CurrentUserService : ICurrentUserService
{
    public string UserId => "example@example.com";
}