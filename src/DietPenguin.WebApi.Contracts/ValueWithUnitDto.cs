namespace DietPenguin.WebApi.Contracts;

public record ValueWithUnitDto(decimal Value, string UnitName, string Abbreviation)
{
}