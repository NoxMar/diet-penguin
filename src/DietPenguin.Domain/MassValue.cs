namespace DietPenguin.Domain;

public record MassValue
{
    public decimal Value { get; private init; }
    public MassUnit Unit { get; private init; }
    public MassValue(decimal value, MassUnit unit)
    {
        if (value < 0M)
        {
            throw new ArgumentException("Must be greater or equal to 0", nameof(value));
        }
        Value = value;
        Unit = unit;
    }

    public MassValue ConvertTo(MassUnit newUnit)
    {
        decimal valueInGrams = Value * Unit.ToGramsRatio;
        return new MassValue(valueInGrams / newUnit.ToGramsRatio, newUnit);
    }
}