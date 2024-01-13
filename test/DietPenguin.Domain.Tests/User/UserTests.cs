using System.Diagnostics.CodeAnalysis;
using DietPenguin.Core.Services;
using DietPenguin.Domain.Nutrition;
using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Tests.User;


public class UserTests
{
    [SuppressMessage("Substitute creation", "NS2002:Constructor parameters count mismatch.")]
    private static readonly INutritionalNeedsService NutritionalNeedsService = Substitute.For<INutritionalNeedsService>();


    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_HeightLowerOrEqual0_ValidationError(decimal height)
    {
        // Arrange
        
        
        // Act
        var result = Domain.User.User.Create(
            DateTime.MaxValue,
            Gender.Female,
            new MassValue(90.0M, MassUnit.Kilogram),
            height,
            Substitute.For<IDateTimeProvider>(),
            NutritionalNeedsService);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeEquivalentTo(UserErrors.HeightLowerThanOrEquals0);
    }

    [Fact]
    public void Create_CorrectData_QueueDomainEvent()
    {
        // Arrange

        // Act
        var result = Domain.User.User.Create(
            DateTime.MaxValue,
            Gender.Male,
            new MassValue(69.96M, MassUnit.Kilogram),
            169M,
            new DateTimeProvider(),
            NutritionalNeedsService
        );

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.DomainEvents.Should().ContainSingle();
    }
}