namespace DietPenguin.WebApi.Contracts;

public record RangeDto(
    decimal Start,
    decimal End,
    string Unit);