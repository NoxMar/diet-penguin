namespace DietPenguin.Domain;

public record Unit
{
    public string Name { get; private init; }
    public string Abbreviation { get; private init; }
    public Unit BaseUnit { get; private init; }
    public decimal ToBaseRatio { get; private init; }
    
    public static readonly Unit Gram = new("Gram", "g");
    public static readonly Unit Kilogram = new("Kilogram", "kg", Gram, 1000M);
    public static readonly Unit Milligram = new("Milligram", "mg", Gram, 0.001M);
    public static readonly Unit Kcal = new("Kilocalorie", "kcal");

    private Unit(string name, string abbreviation)
    {
        Name = name;
        Abbreviation = abbreviation;
        BaseUnit = this;
        ToBaseRatio = 1M;
    }

    private Unit(string name, string abbreviation, Unit baseUnit, decimal toBaseRatio)
    {
        Name = name;
        Abbreviation = abbreviation;
        BaseUnit = baseUnit;
        ToBaseRatio = toBaseRatio;
    }
}