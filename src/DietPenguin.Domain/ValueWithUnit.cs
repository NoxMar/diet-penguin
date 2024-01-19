namespace DietPenguin.Domain;

public record ValueWithUnit(decimal Value, Unit Unit)
{
    public ValueWithUnit ConvertTo(Unit newUnit)
    {
        if (Unit.BaseUnit != newUnit.BaseUnit)
        {
            throw new InvalidOperationException("Units need to have the same `BaseUnit` value for the conversion");
        }
        
        decimal newValue = Value * (Unit.ToBaseRatio / newUnit.ToBaseRatio);
        return new ValueWithUnit(newValue, newUnit);
    }
}