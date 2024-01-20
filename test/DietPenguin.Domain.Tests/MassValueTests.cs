using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Tests;

public class MassValueTests : ValueWithUnitTests
{
    protected override ValueWithUnit CreateSut(decimal value, Unit unit) 
        => new MassValue(value, unit);

    [Fact]
    public void Constructor_ValueLowerThanZero_ArgumentException()
    {
        // Arrange
        // ReSharper disable once ObjectCreationAsStatement
        Action act = () => CreateSut(-2137, Unit.Gram);
        
        // Act
        
        // Assert
        act.Should().Throw<ArgumentException>()
            .WithParameterName("value");
    }
}