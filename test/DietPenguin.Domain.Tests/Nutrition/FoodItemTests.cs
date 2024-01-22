using DietPenguin.Domain.Nutrition;
using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Tests.Nutrition;

public class FoodItemTests
{
    private const string TestName = "Jajko";

    private static IEnumerable<Portion> CreateTestPortions()
    {
        yield return new Portion(Name: "Mała sztuka", new MassValue(53, Unit.Gram));
        yield return new Portion(Name: "Średnia sztuka", Size: new MassValue(57, Unit.Gram));
    }

    private static IEnumerable<KeyValuePair<Nutrient, ValueWithUnit>> CreateNutritionalValues()
    {
        yield return new KeyValuePair<Nutrient, ValueWithUnit>(Nutrient.Protein, new ValueWithUnit(12.2M, Unit.Gram));
        yield return new KeyValuePair<Nutrient, ValueWithUnit>(Nutrient.Fat, new ValueWithUnit(9.9M, Unit.Gram));
    }
    
    [Fact]
    public void Create_CorrectArguments_ReturnSuccessResult()
    {
        // Arrange
        // Act
        var sut = FoodItem.Create(TestName,
            CreateTestPortions(),
            CreateNutritionalValues());
        
        // Assert
        sut.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Create_CorrectArguments_QueueDomainEvent()
    {
        // Arrange
        // Act
        var sut = FoodItem.Create(TestName,
                CreateTestPortions(),
                CreateNutritionalValues())
            .Value;
        
        // Assert
        sut.DomainEvents.Should()
            .ContainEquivalentOf(new FoodItemCreatedEvent(sut.Id));
    }

    [Fact]
    public void Create_CorrectArguments_SetFieldsCorrectly()
    {
        // Arrange
        // Act
        var sut = FoodItem.Create(TestName,
                CreateTestPortions(),
                CreateNutritionalValues())
            .Value;
        
        // Assert
        sut.Name.Should().BeEquivalentTo(TestName);
        foreach (var p in CreateTestPortions())
        {
            sut.TypicalPortions.Should().ContainEquivalentOf(p);
        }
        foreach (var np in CreateNutritionalValues())
        {
            sut.NutritionalValuesPer100G.Should().ContainKey(np.Key);
            sut.NutritionalValuesPer100G[np.Key].Should().BeEquivalentTo(np.Value);
        }
    }
}