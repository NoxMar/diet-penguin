using DietPenguin.Core;

namespace DietPenguin.Application.User;

public static class UserErrors
{
    public static Error UserNotFoundError = new("USER.NOT_FOUND", "User with a given id couldn't be found");
}