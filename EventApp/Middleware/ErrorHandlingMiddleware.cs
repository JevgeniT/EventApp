using System.Net;
using System.Text.Json;
using Domain;

namespace EventApp.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment webHostEnvironment, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, webHostEnvironment, logger);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        Exception ex,
        IWebHostEnvironment webHostEnvironment,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        var code = ex switch
        {
            EventNotFoundException _ => HttpStatusCode.NotFound,
            ArgumentException or ArgumentNullException _ => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var result = JsonSerializer.Serialize(new { error = ex?.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}