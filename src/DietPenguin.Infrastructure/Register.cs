using DietPenguin.Domain.User;
using DietPenguin.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace DietPenguin.Infrastructure;

public static class Register
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<DispatchingContext>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}