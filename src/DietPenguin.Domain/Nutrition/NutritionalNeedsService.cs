using DietPenguin.Core.Services;
using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Nutrition;

public class NutritionalNeedsService : INutritionalNeedsService
{
    // Unless stated otherwise in the method for specific nutrient values are based on OSHAs nutritional goals:
    // https://health.gov/our-work/nutrition-physical-activity/dietary-guidelines/previous-dietary-guidelines/2015/advisory-report/appendix-e-3/appendix-e-31a4
    private static readonly decimal LegalCupToMlRatio = 240;
    public IEnumerable<NutritionalNeed> CalculateNutritionalNeeds(User.User user, IDateTimeProvider dateTimeProvider)
    {
        // For any more nutrients more flexible approach should be considered.
        int age = user.GetAge(dateTimeProvider);
        var caloriesNeed = CalculateCalories(user, age);
        yield return caloriesNeed;
        yield return CalculateProtein(user, age, caloriesNeed);
        yield return CalculateFats(user, age, caloriesNeed);
        yield return CalculateCarbs(user, age, caloriesNeed);
        yield return CalculateFiber(user, age, caloriesNeed);
        yield return CalculateWater(user, age);
        yield return CalculateVitaminC(user, age);
        yield return CalculateVitaminD(user, age);
        yield return CalculateVitaminA(user, age);
        yield return CalculateIron(user, age);
        yield return CalculateCalcium(user, age);
        yield return CalculatePotassium(user, age);
    }

    private NutritionalNeed CalculateCalories(User.User user, int age)
    {
        decimal upperBound = (user.Gender, age) switch
        {
            (_, <= 3) => 1000,
            (Gender.Female, <= 8) => 1200,
            (Gender.Male, <= 8) => 1400,
            (Gender.Female, <= 13) => 1600,
            (Gender.Male, <= 13) => 1800,
            (Gender.Female, <= 18) => 1800,
            (Gender.Male, <= 18) => 2800,
            (Gender.Female, <= 30) => 2000,
            (Gender.Male, <= 30) => 2600,
            (Gender.Female, <= 50) => 1800,
            (Gender.Male,  <= 50) => 2200,
            (Gender.Female, _) => 1600,
            (Gender.Male, _) => 2000,
            _ => throw new ArgumentException("User's gender isn't of any declared enum value")
        };

        return new NutritionalNeed(Nutrient.Energy, 0, upperBound);
    }
    
    private NutritionalNeed CalculateProtein(User.User user, int age, NutritionalNeed caloriesNeed)
    {
        decimal caloriesNeedProteinGrams = caloriesNeed.UpperBound.Value / Nutrient.Protein.EnergyPerPreferred;
        var (proteinPercentLower, proteinPercentHigher) = age switch
        {
            <= 3 => (5M, 20M),
            _ => (10M, 35M)
        };
        var proteinGramsLower = caloriesNeedProteinGrams * (proteinPercentLower / 100);
        var proteinGramsHigher = caloriesNeedProteinGrams * (proteinPercentHigher / 100);
        return new NutritionalNeed(Nutrient.Protein, proteinGramsLower, proteinGramsHigher);
    }
    
    private NutritionalNeed CalculateFats(User.User user, int age, NutritionalNeed caloriesNeed)
    {
        decimal caloriesNeedProteinGrams = caloriesNeed.UpperBound.Value / Nutrient.Fat.EnergyPerPreferred;
        var (proteinPercentLower, proteinPercentHigher) = age switch
        {
            <= 3 => (30M, 40M),
            <= 18 => (25M, 35M),
            _ => (20M, 35M)
        };
        var proteinGramsLower = caloriesNeedProteinGrams * (proteinPercentLower / 100);
        var proteinGramsHigher = caloriesNeedProteinGrams * (proteinPercentHigher / 100);
        return new NutritionalNeed(Nutrient.Fat, proteinGramsLower, proteinGramsHigher);
    }
    
    private NutritionalNeed CalculateCarbs(User.User user, int age, NutritionalNeed caloriesNeed)
    {
        decimal caloriesNeedProteinGrams = caloriesNeed.UpperBound.Value / Nutrient.Carbs.EnergyPerPreferred;
        var proteinGramsLower = caloriesNeedProteinGrams * (45M / 100);
        var proteinGramsHigher = caloriesNeedProteinGrams * (65M / 100);
        return new NutritionalNeed(Nutrient.Carbs, proteinGramsLower, proteinGramsHigher);
    }
    
    private NutritionalNeed CalculateFiber(User.User user, int age, NutritionalNeed caloriesNeed)
    {
        decimal calories = caloriesNeed.UpperBound.Value;
        return new NutritionalNeed(Nutrient.Fiber, 0.014M * calories, 0.028M * calories);
    }


    private NutritionalNeed CalculateWater(User.User user, int age)
    {
        // Lower bound per Harvard School of Public Health due to lack of OSHA recommendations: https://www.hsph.harvard.edu/nutritionsource/water/
        decimal lowerBoundMl = (user.Gender, age) switch
        {
            (_, <= 3) => 4,
            (_, <= 8) => 5,
            (_, <= 13) => 8,
            (_, <= 18) => 11,
            (Gender.Male, _) => 13,
            (Gender.Female, _) => 9,
            _ => throw new ArgumentException("User's gender isn't of any declared enum value"),
        } * LegalCupToMlRatio;
        // No upper bound found
        return new NutritionalNeed(Nutrient.Water, lowerBoundMl, decimal.MaxValue);
    }
    
    private NutritionalNeed CalculateVitaminC(User.User user, int age)
    {
        decimal lowerBoundMgs = (user.Gender, age) switch
        {
            (_, <= 3) => 15,
            (_, <= 8) => 25,
            (_, <= 14) => 45,
            (Gender.Female, <= 18) => 65,
            (Gender.Male, <= 18) => 75,
            (Gender.Female, _) => 75,
            (Gender.Male, _) => 90,
            _ => throw new ArgumentException("User's gender isn't of any declared enum value"),
        };
        return new NutritionalNeed(Nutrient.VitC, lowerBoundMgs, 1000);
    }
    
    private NutritionalNeed CalculateVitaminD(User.User user, int age)
    {
        // Lower bound per guidelines is 600UI, IU = 0.025ug thus 600IU * 0.025ug/UI = 15ug
        // Upper bound per Harvard School of Public Health: https://www.hsph.harvard.edu/nutritionsource/vitamin-d/
        return new NutritionalNeed(Nutrient.VitD, 15, 100);
    }
    
    private NutritionalNeed CalculateVitaminA(User.User user, int age)
    {
        decimal lowerBound = (user.Gender, age) switch
        {
            (_, <= 3) => 300,
            (_, <= 8) => 400,
            (_, <= 13) => 600,
            (Gender.Female, _) => 700,
            (Gender.Male, _) => 900,
            _ => throw new ArgumentException("User's gender isn't of any declared enum value"),
        };
        // Upper bound per Harvard School of Public Health: https://www.hsph.harvard.edu/nutritionsource/vitamin-a/
        return new NutritionalNeed(Nutrient.VitA, lowerBound, 3000);
    }
    
    private NutritionalNeed CalculateIron(User.User user, int age)
    {
        decimal lowerMgs = (user.Gender, age) switch
        {
            (_, <= 3) => 7,
            (_, <= 8) => 10,
            (_, <= 13) => 8,
            (Gender.Female, <= 18) => 15,
            (Gender.Male, <= 18) => 11,
            (Gender.Female, <= 50) => 18,
            (Gender.Male, <= 50) => 8,
            _ => 8
        };
        return new NutritionalNeed(Nutrient.Iron, lowerMgs, 45);
    }
    
    private NutritionalNeed CalculateCalcium(User.User user, int age)
    {
        decimal lowerBound = age switch
        {
            <= 3 => 700,
            <= 8 => 100,
            <= 18 => 1300,
            <= 50 => 1000,
            _ => 1200,
        };
        // Upper bound as per Harvard School of Public Health: https://www.hsph.harvard.edu/nutritionsource/calcium/
        return new NutritionalNeed(Nutrient.Calcium, lowerBound, age <= 50 ? 2500 : 2000);
    }
    
    private NutritionalNeed CalculatePotassium(User.User user, int age)
    {
        decimal lowerBoundMgs = age switch
        {
            <= 3 => 3000,
            <= 8 => 3800,
            <= 13 => 4500,
            _ => 4700
        };
        
        // Couldn't find legitimate upper bound, so assumed 50_000 mg as value
        // order of magnitude higher that highest recommended value based on some case studies:
        // https://ods.od.nih.gov/factsheets/Potassium-HealthProfessional/
        return new NutritionalNeed(Nutrient.Potassium, lowerBoundMgs, 50_000M);
    }
}