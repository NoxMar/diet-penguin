using DietPenguin.Core.Services;
using DietPenguin.Domain.Nutrition;
using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Tests.Nutrition;

public class NutritionalNeedsServiceTests
{
    // Due to excessive burden of testing all of the combination of age range, gender, and nutrient following approach
    // was chosen: testing each nutrient for at least one age range/gender combination.
    
    private static readonly DateTime StartingDate = new DateTime(2000, 1, 1);
    private readonly IDateTimeProvider _startingDateProvider = Substitute.For<IDateTimeProvider>();

    public NutritionalNeedsServiceTests()
    {
        _startingDateProvider.UtcNow.Returns(StartingDate);
    }
    
    
    [Fact]
    public void CalculateNutritionalNeeds_For40YearsOldFemale_CalculateProperValues()
    {
        // Arrange
        var sut = new NutritionalNeedsService();
        // Act
        var needs = Domain.User.User.Create(
            StartingDate.AddYears(-40),
            Gender.Female,
            new MassValue(55, Unit.Kilogram),
            171,
            _startingDateProvider,
            sut
        ).Value.NutritionalNeeds;
        
        // Assert
        needs.Should().ContainKey(Nutrient.Energy);
        needs[Nutrient.Energy].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Energy, 0, 1800));
        needs.Should().ContainKey(Nutrient.Fat);
        needs[Nutrient.Fat].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Fat, 40M, 70M));
        needs.Should().ContainKey(Nutrient.VitD);
        needs[Nutrient.VitD].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.VitD, 15, 100));
        needs.Should().ContainKey(Nutrient.Calcium);
        needs[Nutrient.Calcium].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Calcium, 1000, 2500));
    }
    
    [Fact]
    public void CalculateNutritionalNeeds_For15YearsOldMale_CalculateProperValues()
    {
        // Arrange
        var sut = new NutritionalNeedsService();
        // Act
        var needs = Domain.User.User.Create(
            StartingDate.AddYears(-15),
            Gender.Male,
            new MassValue(65, Unit.Kilogram),
            171,
            _startingDateProvider,
            sut
        ).Value.NutritionalNeeds;
        
        // Assert
        needs.Should().ContainKey(Nutrient.Protein);
        needs[Nutrient.Protein].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Protein, 70, 245));
        needs.Should().ContainKey(Nutrient.Fiber);
        needs[Nutrient.Fiber].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Fiber, 39.2M, 78.4M));
        needs.Should().ContainKey(Nutrient.VitA);
        needs[Nutrient.VitA].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.VitA, 900, 3000));
        needs.Should().ContainKey(Nutrient.Potassium);
        needs[Nutrient.Potassium].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Potassium, 4700, 50_000));
    }
    
    [Fact]
    public void CalculateNutritionalNeeds_For3YearsOldChild_CalculateProperValues()
    {
        // Arrange
        var sut = new NutritionalNeedsService();
        // Act
        var needs = Domain.User.User.Create(
            StartingDate.AddYears(-3),
            Gender.Male,
            new MassValue(14.5M, Unit.Kilogram),
            95.3M,
            _startingDateProvider,
            sut
        ).Value.NutritionalNeeds;
        
        // Assert
        needs.Should().ContainKey(Nutrient.Carbs);
        needs[Nutrient.Carbs].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Carbs, 112.5M, 162.5M));
        needs.Should().ContainKey(Nutrient.VitC);
        needs[Nutrient.VitC].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.VitC, 15, 1000));
        needs.Should().ContainKey(Nutrient.Iron);
        needs[Nutrient.Iron].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Iron, 7, 45));
        needs.Should().ContainKey(Nutrient.Water);
        needs[Nutrient.Water].Should()
            .BeEquivalentTo(new NutritionalNeed(Nutrient.Water, 960, 20_000M));
    }

}