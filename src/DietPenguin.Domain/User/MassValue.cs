namespace DietPenguin.Domain.User;

public record MassValue : ValueWithUnit
{
    public MassValue(decimal value, Unit unit)
        : base(value, unit)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "must be greater that or equal to 0");
        }

        if (Unit.BaseUnit != Unit.Gram)
        {
            throw new ArgumentException("Unit must be based on grams", nameof(unit));
        }
    }
}