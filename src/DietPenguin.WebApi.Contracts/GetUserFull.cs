namespace DietPenguin.WebApi.Contracts;

public class GetUserFull : IRequest<Result<GetUserFull.Response>>
{
    public const string RouteTemplate = "/users/me";

    public record Response();
}