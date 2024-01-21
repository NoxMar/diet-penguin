using DietPenguin.Domain.Nutrition;
using Microsoft.Extensions.DependencyInjection;

namespace DietPenguin.Domain;

public static class Register
{
    public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<INutritionalNeedsService, NutritionalNeedsService>();
        return services;
    }
}