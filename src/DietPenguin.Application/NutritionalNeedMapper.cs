using DietPenguin.Domain;
using DietPenguin.Domain.Nutrition;
using DietPenguin.Domain.User;
using DietPenguin.WebApi.Contracts;

namespace DietPenguin.Application;

public class NutritionalNeedMapper : INutritionalNeedMapper
{
    public NutritionalNeedDto FromValueObject(NutritionalNeed need)
    {
        return new NutritionalNeedDto(
            Nutrient: new NutrientDto(
                Name: need.Nutrient.Name,
                PreferredUnit: need.Nutrient.PreferredUnit.Abbreviation),
            RecommendedRange: new RangeDto(
                Start: need.LowerBound.Value,
                End: need.UpperBound.Value,
                Unit: need.LowerBound.Unit.Abbreviation)
            );
    }
}