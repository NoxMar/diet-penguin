using DietPenguin.Application.User;
using DietPenguin.Domain.User;
using Microsoft.Extensions.DependencyInjection;

namespace DietPenguin.Application;

public static class Register
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<INutritionalNeedMapper, NutritionalNeedMapper>();
        services.AddSingleton<IUserMapper, UserMapper>();
        return services;
    }
}