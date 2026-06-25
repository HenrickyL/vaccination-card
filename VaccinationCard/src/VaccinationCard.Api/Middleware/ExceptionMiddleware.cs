using VaccinationCard.Domain.Enums;
using VaccinationCard.Domain.Exceptions;

namespace VaccinationCard.Api.Middlewares;

public record ErrorResponse(
    int Status,
    string Code,
    string Details,
    string? Field = null
);

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException ex)
        {
            await HandleDomainAsync(context, ex, logger);
        }
        catch (FluentValidation.ValidationException fve) {

            var response = new ErrorResponse(
            Status: 400,
            Code: ErrorCode.ValidationError.ToString(),
            Details: string.Join("; ", fve.Errors.Select(e => e.ErrorMessage)),
            Field: fve.Errors.FirstOrDefault()?.PropertyName
            );

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await context.Response.WriteAsJsonAsync(
                new ErrorResponse(500, "InternalError", "Internal server error")
            );
        }
    }

    private static Task HandleDomainAsync(HttpContext ctx, DomainException ex, ILogger logger)
    {
        var statusCode = ex switch
        {
            NotFoundException => 404,
            BadRequestException => 400,
            ValidationException => 400,
            ForbiddenException => 403,
            _ => 500
        };

        if (statusCode == 500)
            logger.LogError(ex, "Unhandled domain exception");

        ctx.Response.StatusCode = statusCode;
        ctx.Response.ContentType = "application/json";

        var response = new ErrorResponse(
            Status: statusCode,
            Code: ex.ErrorCode.ToString(),
            Details: ex.Details,
            Field: (ex is ValidationException ve) ? ve.Field : null
        );

        return ctx.Response.WriteAsJsonAsync(response);
    }
}