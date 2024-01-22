namespace DietPenguin.Domain;

public record Unit
{
    public string Name { get; private init; }
    public string Abbreviation { get; private init; }
    public Unit BaseUnit { get; private init; }
    public decimal ToBaseRatio { get; private init; }
    
    public static readonly Unit Gram = new(nameof(Gram), "g");
    public static readonly Unit Kilogram = new(nameof(Kilogram), "kg", Gram, 1000M);
    public static readonly Unit Milligram = new(nameof(Milligram), "mg", Gram, 0.001M);
    public static readonly Unit Microgram = new(nameof(Microgram), "μg", Gram, 0.000001M);
    public static readonly Unit Milliliter = new(nameof(Milliliter), "mL");
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

    public virtual bool Equals(Unit? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Abbreviation == other.Abbreviation;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Abbreviation);
    }

    public override string ToString()
    {
        return $"Unit({Name}, {Abbreviation}, {BaseUnit.Name}, {ToBaseRatio})";
    }
}