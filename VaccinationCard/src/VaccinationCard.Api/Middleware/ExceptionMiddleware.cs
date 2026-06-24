using VaccinationCard.Domain.Exceptions;

namespace VaccinationCard.Api.Middlewares;

public record ErrorResponse(int Status, object Message);
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
        catch (Exception ex)
        {
            await HandleAsync(context, ex, logger);
        }
    }

    private static Task HandleAsync(HttpContext ctx, Exception ex, ILogger logger)
    {
        // TODO: use DomainExceptions
        var (statusCode, message) = ex switch
        {
            NotFoundException e => (404, e.Message),
            BadRequestException e => (400, e.Message),
            //ForbiddenException e => (403, e.Message),
            //ValidationException e => (400, FormatValidation(e)),
            _ => (500, "Internal server error")
        };

        if (statusCode == 500)
            logger.LogError(ex, "Unhandled exception");

        ctx.Response.StatusCode = statusCode;
        ctx.Response.ContentType = "application/json";

        return ctx.Response.WriteAsJsonAsync(new ErrorResponse(statusCode, message));
    }

    //private static string FormatValidation(ValidationException ex) =>
    //    string.Join("; ", ex.Errors.Select(e => e.ErrorMessage));
}