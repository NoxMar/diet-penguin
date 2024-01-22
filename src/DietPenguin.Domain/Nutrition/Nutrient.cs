namespace DietPenguin.Domain.Nutrition;

public record Nutrient(string Name, Unit PreferredUnit, decimal EnergyPerPreferred = 0)
{
    public static readonly Nutrient Energy = new("Energia", Unit.Kcal);
    public static readonly Nutrient Protein = new("Białko", Unit.Gram, 4);
    public static readonly Nutrient Carbs = new("Węglowodany", Unit.Gram, 4);
    public static readonly Nutrient Fat = new ("Tłuszcz", Unit.Gram, 9);
    public static readonly Nutrient Fiber = new("Błonnik", Unit.Gram);
    public static readonly Nutrient VitC = new("Witamina C", Unit.Milligram);
    public static readonly Nutrient VitD = new("Witamina D", Unit.Microgram);
    public static readonly Nutrient VitA = new("Witamina A", Unit.Milligram);
    public static readonly Nutrient Iron = new("Żelazo", Unit.Milligram);
    public static readonly Nutrient Calcium = new("Wapń", Unit.Milligram);
    public static readonly Nutrient Potassium = new("Potas", Unit.Milligram);
    public static readonly Nutrient Water = new("Woda", Unit.Milliliter);
}