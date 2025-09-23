using Application.Abstracts;
using Application.Mappers;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationService
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddScoped<TokenService>();

        return services;
    }
}
