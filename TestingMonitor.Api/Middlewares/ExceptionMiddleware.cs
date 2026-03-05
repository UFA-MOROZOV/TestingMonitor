using System.Text.Json;
using TestingMonitor.Domain.Exceptions;

namespace TestingMonitor.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ApiException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var errorResponse = new ExceptionResponse
            {
                Message = ex.Message,
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;

            context.Response.ContentType = "application/json";

            var errorResponse = new ExceptionResponse
            {
                Message = "Произошла ошибка сервера.",
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }
}

/// <summary>
/// Api exception response.
/// </summary>
public sealed class ExceptionResponse
{
    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; set; } = null!;
}