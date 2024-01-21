using DietPenguin.Domain;
using DietPenguin.WebApi.Contracts;

namespace DietPenguin.Application.User;

public class UserMapper : IUserMapper
{
    private readonly INutritionalNeedMapper _nutritionalNeedMapper;

    public UserMapper(INutritionalNeedMapper nutritionalNeedMapper)
    {
        _nutritionalNeedMapper = nutritionalNeedMapper;
    }

    public UserDto FromEntity(Domain.User.User user)
    {
        return new UserDto(
            DateOfBirth: user.DateOfBrith,
            Gender: user.Gender.ToString(),
            MassKg: user.Weight.ConvertTo(Unit.Kilogram).Value,
            HeightCm: user.HeightCm,
            NutritionalNeeds: user.NutritionalNeeds
                .ToDictionary(u => u.Key.Name,
                    u => _nutritionalNeedMapper.FromValueObject(u.Value))
        );
    }
}