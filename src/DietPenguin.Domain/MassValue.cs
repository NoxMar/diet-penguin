namespace DietPenguin.Domain;

public record MassValue
{
    public decimal Value { get; private init; }
    public MassUnit Unit { get; private init; }
    public MassValue(decimal value, MassUnit unit)
    {
    }

    public MassValue ConvertTo(MassUnit newUnit)
    {
        throw new NotImplementedException("Added for red test");
    }
}