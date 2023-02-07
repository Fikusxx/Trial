using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Trial.Core.Common.Exceptions;

namespace Trial.WebAPI.Middleware;

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
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = exception.Message });

        switch (exception)
        {
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                break;

            default:
                code = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
