namespace DietPenguin.Domain;

public record Unit(string Name, string Abbreviation)
{
    public static readonly Unit Kcal = new Unit("kilocalorie", "kcal");
}