namespace DietPenguin.WebApi.Contracts.User;

public record UserDto(
    DateTime DateOfBirth,
    string Gender,
    decimal MassKg,
    decimal HeightCm,
    Dictionary<string, NutritionalNeedDto> NutritionalNeeds
    );