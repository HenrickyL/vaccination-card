using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi;
using VaccinationCard.Api.Extensions;
using VaccinationCard.Infrastructure;
using VaccinationCard.Application;


namespace VaccinationCard.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load("../../.env");

        // Services
        builder.Services.AddControllers();
        // JWT Authentication
        builder.Services.AddJwtAuthentication(builder.Configuration);
        // Infrastructure
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        SetupOpenApiWithSwagger(builder);

        var app = builder.Build();

        // Middleware
        app.UseApiExceptions();
        app.UseHttpsRedirection();
        // JWT Middleware
        app.UseJwtAuthentication();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "API v1");
            });
        }

        app.MapControllers();

        app.Run();
    }

    private static void SetupOpenApiWithSwagger(WebApplicationBuilder builder) {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer(
                async (
                    document,
                    context,
                    cancellationToken
                ) =>
                {
                    var authProvider =
                        context.ApplicationServices
                            .GetRequiredService<IAuthenticationSchemeProvider>();

                    var schemes =
                        await authProvider
                            .GetAllSchemesAsync();

                    if (
                        schemes.Any(
                            x => x.Name == "Bearer"
                        )
                    )
                    {
                        document.Components ??=
                            new();

                        document.Components.SecuritySchemes =
                            new Dictionary<
                                string,
                                IOpenApiSecurityScheme>
                            {
                                ["Bearer"] =
                                    new OpenApiSecurityScheme
                                    {
                                        Type =
                                            SecuritySchemeType.Http,

                                        Scheme =
                                            "bearer",

                                        In =
                                            ParameterLocation.Header,

                                        BearerFormat =
                                            "JWT"
                                    }
                            };
                    }
                });
        });
    }
}