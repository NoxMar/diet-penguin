using DietPenguin.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DietPenguin.Core;

public static class Register
{
    public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}