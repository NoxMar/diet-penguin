using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Tests;

public class ValueWithUnitTests
{

    [Fact]
    public void Constructor_ValueLowerThanZero_ArgumentException()
    {
        // Arrange
        // ReSharper disable once ObjectCreationAsStatement
        Action act = () => new MassValue(-2137, Unit.Gram);
        
        // Act
        
        // Assert
        act.Should().Throw<ArgumentException>()
            .WithParameterName("value");
    }

    [Fact]
    public void ConvertTo_SameUnit_ReturnSameValue()
    {
        // Arrange
        MassValue sut = new MassValue(1337, Unit.Kilogram);
        
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
        var expected = new MassValue(decimal.Parse(expectedGramsString), Unit.Gram);
        var sut = new MassValue(kilogramsValue, Unit.Kilogram);
        
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
        var sut = new MassValue(kilogramsValue, Unit.Kilogram);
        var expected = new MassValue(milligramsValue, Unit.Milligram);
        
        // Act
        var result = sut.ConvertTo(Unit.Milligram);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}