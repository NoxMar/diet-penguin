using DietPenguin.Core.Services;
using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Nutrition;

public interface INutritionalNeedsService
{
    IEnumerable<NutritionalNeed> CalculateNutritionalNeeds(User.User user, IDateTimeProvider dateTimeProvider);
}