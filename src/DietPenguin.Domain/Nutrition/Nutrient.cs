namespace DietPenguin.Domain.Nutrition;

public record Nutrient(string Name, Unit PreferredUnit, decimal EnergyPerPreferred = 0)
{
    public static readonly Nutrient Energy = new("Energy", Unit.Kcal, 1);
    public static readonly Nutrient Protein = new("Protein", Unit.Gram, 4);
    public static readonly Nutrient Carbs = new("Carbohydrates", Unit.Gram, 4);
    public static readonly Nutrient Fat = new ("Fat", Unit.Gram, 9);
    public static readonly Nutrient Fiber = new("Fiber", Unit.Gram);
    public static readonly Nutrient VitC = new("Vitamin C", Unit.Milligram);
    public static readonly Nutrient VitD = new("Vitamin D", Unit.Microgram);
    public static readonly Nutrient VitA = new("Vitamin A", Unit.Milligram);
    public static readonly Nutrient Iron = new("Iron", Unit.Milligram);
    public static readonly Nutrient Calcium = new("Calcium", Unit.Milligram);
    public static readonly Nutrient Potassium = new("Potassium", Unit.Milligram);
    public static readonly Nutrient Water = new("Water", Unit.Milliliter);
}