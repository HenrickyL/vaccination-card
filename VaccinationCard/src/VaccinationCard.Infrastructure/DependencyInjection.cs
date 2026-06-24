using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaccinationCard.Infrastructure.Persistence;

namespace VaccinationCard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = BuildConnectionString();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    private static string BuildConnectionString()
    {
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var db = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

        return $"Host={host};Port={port};Database={db};Username={user};Password={pass}";
    }
}