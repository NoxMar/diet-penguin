using DietPenguin.Core;
using DietPenguin.Core.Services;
using DietPenguin.Domain.Nutrition;

namespace DietPenguin.Domain.User;

public class User : BaseEntity
{
    public DateTime DateOfBrith { get; private set; }
    public Gender Gender { get; private set; }
    
    public MassValue Weight { get; private set; }
    
    public decimal HeightCm { get; private set; }

    private User(DateTime dateOfBrith, Gender gender, MassValue weight, decimal heightCm)
    {
        HeightCm = heightCm;
        DateOfBrith = dateOfBrith;
        Gender = gender;
        Weight = weight;
    }

    public int GetAge(IDateTimeProvider dateTimeProvider)
    {
        var currentDate = dateTimeProvider.UtcNow;

        int years = currentDate.Year - DateOfBrith.Year;
        
        if (DateOfBrith.Month > currentDate.Month ||
            (DateOfBrith.Month == currentDate.Month && DateOfBrith.Day > currentDate.Day))
        {
            years -= 1;
        }
        
        return years;
    }

    public static Result<User> Create(
        DateTime dateOfBirth,
        Gender gender,
        MassValue weight,
        decimal heightCm,
        IDateTimeProvider dateTimeProvider,
        INutritionalNeedsService nutritionalNeedsService
    )
    {
        if (heightCm <= 0)
        {
            return Result<User>.Failure(UserErrors.HeightLowerThanOrEquals0);
        }
        var user = new User(dateOfBirth, gender, weight, heightCm);
        
        user.QueueDomainEvent(new UserCreatedEvent(user.Id));
        return Result<User>.Success(user);
    }
}