namespace DietPenguin.Core.Tests.Services;

public class DateTimeProviderTests
{
    [Fact]
    public void UtcNow_ReturnsTimeWithKindUtc()
    {
        // Arrange
        IDateTimeProvider sut = new DateTimeProvider();
        
        // Act
        var result = sut.UtcNow;
        
        // Assert
        result.Kind.Should().Be(DateTimeKind.Utc);
    }

    [Fact]
    public void UtcNow_ReturnsCurrentTime()
    {
        // Arrange
        IDateTimeProvider sut = new DateTimeProvider();

        // Act
        var result = sut.UtcNow;
        
        // Assert
        result.Should().BeCloseTo(DateTime.UtcNow,
            precision: TimeSpan.FromMilliseconds(1000));
    }
}