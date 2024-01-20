namespace DietPenguin.Domain.Nutrition;

public record Nutrient(string Name, Unit PreferredUnit)
{
    public static readonly Nutrient Energy = new("Energy", Unit.Kcal);
}