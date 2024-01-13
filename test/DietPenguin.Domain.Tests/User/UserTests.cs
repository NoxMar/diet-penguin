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
    
    [Fact]
    public void GetAge_SameDayAndMonth_DifferenceInYears()
    {
        // Arrange
        var currentDate = new DateTime(1337, 11, 9);
        var dateTimeSource = Substitute.For<IDateTimeProvider>();
        dateTimeSource.UtcNow.Returns(currentDate);
        
        var sut = Domain.User.User.Create(
            currentDate.AddYears(-10),
            Gender.Female,
            new MassValue(69.96M, MassUnit.Kilogram),
            169M,
            dateTimeSource,
            NutritionalNeedsService
        ).Value;
        
        // Act
        var result = sut.GetAge(dateTimeSource);
        
        // Assert
        result.Should().Be(10);
    }

    [Theory]
    [InlineData(10, 8)]
    [InlineData(10, 9)]
    [InlineData(10, 10)]
    [InlineData(11, 8)]
    public void GetAge_BeforeBirthdayInCurrentYear_DifferenceInYearsMinus1(int month, int day)
    {
        // Arrange
        int currentYear = 1337;
        var currentDate = new DateTime(currentYear, month, day);
        var dateTimeSource = Substitute.For<IDateTimeProvider>();
        dateTimeSource.UtcNow.Returns(currentDate);
        
        var sut = Domain.User.User.Create(
            new DateTime(currentYear - 10, 11, 9),
            Gender.Female,
            new MassValue(69.96M, MassUnit.Kilogram),
            169M,
            dateTimeSource,
            NutritionalNeedsService
        ).Value;
        
        // Act
        var age = sut.GetAge(dateTimeSource);
        
        // Assert
        age.Should().Be(9);
    }
}