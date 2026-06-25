using DotNetEnv;
using VaccinationCard.Api.Extensions;
using VaccinationCard.Application;
using VaccinationCard.Infrastructure;
using VaccinationCard.Api.Transformers;

namespace VaccinationCard.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load("../../.env");

        builder.Services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
        // Services
        builder.Services.AddControllers();
        // JWT Authentication
        builder.Services.AddJwtAuthentication(builder.Configuration);
        // Infrastructure
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        var app = builder.Build();

        // Middleware
        app.UseApiExceptions();
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "VaccinationCard API v1");
                options.RoutePrefix = "swagger";
            });
        }

        app.MapControllers();

        app.Run();
    }
}