using DietPenguin.Domain.Nutrition;

namespace DietPenguin.Domain.User;

public record NutritionalNeed
{
    public Nutrient Nutrient { get; private init; }
    public ValueWithUnit LowerBound { get; private init; }
    public ValueWithUnit UpperBound { get; private init; }
    
    public NutritionalNeed(Nutrient nutrient, decimal lowerBound, decimal upperBound)
        : this(
            nutrient,
            new ValueWithUnit(lowerBound, nutrient.PreferredUnit),
            new ValueWithUnit(upperBound, nutrient.PreferredUnit)
        )
    {
    }

    public NutritionalNeed(Nutrient Nutrient, ValueWithUnit LowerBound, ValueWithUnit UpperBound)
    {
        if (LowerBound > UpperBound)
        {
            throw new ArgumentException($"${nameof(LowerBound)} has to be lower than or equal ${nameof(UpperBound)}");
        }
        
        this.Nutrient = Nutrient;
        this.LowerBound = LowerBound;
        this.UpperBound = UpperBound;
    }
}