using DietPenguin.Core;
using MediatR;

using DietPenguin.Core.Services;
using DietPenguin.Domain.Nutrition;
using DietPenguin.Domain.User;
using Unit = DietPenguin.Domain.Unit;

namespace DietPenguin.Application.User;

public class GetCurrentUserRequestHandler : IRequestHandler<GetCurrentUserRequest, Result<UserDto>>
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly INutritionalNeedsService _nutritionalNeedsService;

    private readonly IUserMapper _userMapper;

    public GetCurrentUserRequestHandler(
        IDateTimeProvider dateTimeProvider,
        INutritionalNeedsService nutritionalNeedsService, IUserMapper userMapper)
    {
        _dateTimeProvider = dateTimeProvider;
        _nutritionalNeedsService = nutritionalNeedsService;
        _userMapper = userMapper;
    }

    public async Task<Result<UserDto>> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var result = Domain.User.User.Create(
            _dateTimeProvider.UtcNow.AddYears(-40),
            Gender.Male,
            new MassValue(65.96M, Unit.Kilogram),
            169M,
            new DateTimeProvider(),
            _nutritionalNeedsService
            );
        if (!result.IsSuccess)
        {
            return Result<UserDto>.Failure(UserErrors.UserNotFoundError);
        }

        return Result<UserDto>.Success(_userMapper.FromEntity(result.Value));
    }
}