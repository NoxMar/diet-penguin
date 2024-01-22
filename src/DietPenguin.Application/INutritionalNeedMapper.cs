using DietPenguin.Domain.User;
using DietPenguin.WebApi.Contracts;

namespace DietPenguin.Application;

public interface INutritionalNeedMapper
{
    NutritionalNeedDto FromValueObject(NutritionalNeed need);
}