using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Queries;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarSaleRepository, CarSaleRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IModificationScheduleRepository, ModificationScheduleRepository>();

        services.AddScoped<ICarQueries, CarQueries>();
        services.AddScoped<ICarSaleQueries, CarSaleQueries>();
        services.AddScoped<ICustomerQueries, CustomerQueries>();
        services.AddScoped<IModificationScheduleQueries, ModificationScheduleQueries>();

        return services;
    }
}