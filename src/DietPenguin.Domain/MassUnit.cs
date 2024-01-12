namespace DietPenguin.Domain;

public record MassUnit(string Name, string Abbreviation, decimal ToGramsRatio)
{
    public static readonly MassUnit Gram = new("Gram", "g", 1M);
    public static readonly MassUnit Kilogram = new("Kilogram", "kg", 1000M);
    public static readonly MassUnit Milligram = new("Milligram", "mg", 0.001M);
}