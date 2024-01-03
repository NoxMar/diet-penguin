namespace DietPenguin.Core.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}