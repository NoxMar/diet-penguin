namespace DietPenguin.Domain.User;

public interface ICurrentUserService
{
    string? UserId { get; }
}