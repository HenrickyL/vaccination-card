using DotNetEnv;
using VaccinationCard.Api.Extensions;
using VaccinationCard.Infrastructure;

namespace VaccinationCard.Api;
public class Program{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load("../../.env");

        builder.Services.AddOpenApi();
        // Services
        builder.Services.AddControllers();
        
        // Infrastructure
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Middleware

        app.UseApiExceptions();
        app.UseHttpsRedirection();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();

        app.Run();
    }
}