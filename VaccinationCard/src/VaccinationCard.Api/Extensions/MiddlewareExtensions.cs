using VaccinationCard.Api.Middlewares;

namespace VaccinationCard.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder
    UseApiExceptions(
        this IApplicationBuilder app
    )
    {
        return app.UseMiddleware<
            ExceptionMiddleware
        >();
    }
}