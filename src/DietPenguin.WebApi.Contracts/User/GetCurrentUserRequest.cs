namespace DietPenguin.WebApi.Contracts.User;

public record GetCurrentUserRequest() : IRequest<Result<UserDto>>;