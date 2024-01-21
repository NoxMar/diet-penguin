namespace DietPenguin.WebApi.Contracts;

public record NutritionalNeedDto
(
    NutrientDto Nutrient,
    RangeDto RecommendedRange
);