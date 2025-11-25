using Application.Abstracts;
using Infrastructure.Context;
using Infrastructure.InfrastrcurtureServices;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection RegisterInfraStructureService(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x => x.MigrationsHistoryTable("migrations"));
        });

        services.AddScoped<UserRepository>();

        services.AddScoped<CenterRepository>();

        services.AddScoped<IDataValidationService, DataValidationSerive>();

        services.AddScoped<ScienceRepository>();

        services.AddScoped<GroupRepository>();

        services.AddScoped<StudentRepository>();

        services.AddScoped<AuthRepository>();

        services.AddScoped<PaymentRepository>();

        services.AddScoped<CenterStatisticRepository>();
        return services;
    }
}
