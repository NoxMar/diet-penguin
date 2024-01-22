using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Tests;

public class ValueWithUnitTests
{
    protected virtual ValueWithUnit CreateSut(decimal value, Unit unit) 
        => new(value, unit);
    

    [Fact]
    public void ConvertTo_SameUnit_ReturnSameValue()
    {
        // Arrange
        var sut = CreateSut(1337, Unit.Kilogram);
        
        // Act
        var result = sut.ConvertTo(Unit.Kilogram);
        
        // Assert
        result.Should().BeEquivalentTo(sut);
    }

    [Theory]
    [InlineData("0", "0")]
    [InlineData("2137", "2137000")]
    [InlineData("21.37", "21370")]
    public void ConvertTo_TargetIsGrams_ReturnCorrectValueInGrams(
        string kilogramsValueString,
        string expectedGramsString
    )
    {
        // Arrange
        decimal kilogramsValue = decimal.Parse(kilogramsValueString);
        var expected = CreateSut(decimal.Parse(expectedGramsString), Unit.Gram);
        var sut = CreateSut(kilogramsValue, Unit.Kilogram);
        
        // Act
        var result = sut.ConvertTo(Unit.Gram);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("0", "0")]
    [InlineData("2137", "2137000000")]
    [InlineData("0.0000002137", "0.2137")]
    public void ConvertTo_TargetIsDifferentThanGrams_ReturnCorrectValueInGrams(
        string kilogramsValueStr,
        string milligramsValueStr
    )
    {
        // Arrange
        decimal kilogramsValue = decimal.Parse(kilogramsValueStr);
        decimal milligramsValue = decimal.Parse(milligramsValueStr);
        var sut = CreateSut(kilogramsValue, Unit.Kilogram);
        var expected = CreateSut(milligramsValue, Unit.Milligram);
        
        // Act
        var result = sut.ConvertTo(Unit.Milligram);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}