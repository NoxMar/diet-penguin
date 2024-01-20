using DietPenguin.Domain.Nutrition;
using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Tests.User;

public class NutritionalNeedTests
{
    [Fact]
    public void Constructor_LowerBoundHigherThanUpper_ThrowArgumentException()
    {
        // Arrange
        // ReSharper disable once ObjectCreationAsStatement
        Action act = () => new NutritionalNeed(Nutrient.Energy, 100, 10);
        
        // Act
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_BoundValuesProperRelation_CreateObject()
    {
        // Arrange
        var expectedNutrient = Nutrient.Energy;
        var expectedLowerBound = new ValueWithUnit(1800, Unit.Kcal);
        var expectedUpperBound = new ValueWithUnit(2000, Unit.Kcal);
        
        // Act

        var sut = new NutritionalNeed(Nutrient.Energy, 1800, 2000);
        // Assert

        sut.Nutrient.Should().BeEquivalentTo(expectedNutrient);
        sut.LowerBound.Should().BeEquivalentTo(expectedLowerBound);
        sut.UpperBound.Should().BeEquivalentTo(expectedUpperBound);
    }
}