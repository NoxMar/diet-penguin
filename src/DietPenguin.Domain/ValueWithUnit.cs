namespace DietPenguin.Domain;

public record ValueWithUnit(decimal Value, Unit Unit) : IComparable<ValueWithUnit>
{
    private decimal BaseUnitValue => Value * Unit.ToBaseRatio;
    public ValueWithUnit ConvertTo(Unit newUnit)
    {
        EnsureSameBaseUnitOrThrow(newUnit);
        return new ValueWithUnit(BaseUnitValue / newUnit.ToBaseRatio, newUnit);
    }

    private void EnsureSameBaseUnitOrThrow(Unit newUnit)
    {
        if (Unit.BaseUnit != newUnit.BaseUnit)
        {
            throw new InvalidOperationException("Units need to have the same `BaseUnit` value for the conversion");
        }
    }

    public int CompareTo(ValueWithUnit? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        EnsureSameBaseUnitOrThrow(other.Unit);
        return BaseUnitValue.CompareTo(other.BaseUnitValue);
    }

    public static bool operator <(ValueWithUnit? left, ValueWithUnit? right)
    {
        return Comparer<ValueWithUnit>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(ValueWithUnit? left, ValueWithUnit? right)
    {
        return Comparer<ValueWithUnit>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(ValueWithUnit? left, ValueWithUnit? right)
    {
        return Comparer<ValueWithUnit>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(ValueWithUnit? left, ValueWithUnit? right)
    {
        return Comparer<ValueWithUnit>.Default.Compare(left, right) >= 0;
    }
}