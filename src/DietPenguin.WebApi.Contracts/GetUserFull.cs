namespace DietPenguin.WebApi.Contracts;

public class GetUserFull : IRequest<Result<GetUserFull.Response>>
{
    public const string RouteTemplate = "/api/trails";

    public record Response();
}