using System.Net;
using FluentValidation;
using Newtonsoft.Json;

namespace DeviceService.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleGlobalExceptionAsync(context, ex);
        }
    }

    private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        string result;

        if (exception is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            var errors = validationException.Errors.Select(e => new
            {
                Field = e.PropertyName,
                Error = e.ErrorMessage,
            });

            result = JsonConvert.SerializeObject(
                new { error = "Validation failed", errors = errors }
            );
        }
        else
        {
            if (exception is KeyNotFoundException)
                code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedAccessException)
                code = HttpStatusCode.Unauthorized;
            else if (exception is ArgumentException || exception is ArgumentNullException)
                code = HttpStatusCode.BadRequest;
            else if (exception is InvalidOperationException)
                code = HttpStatusCode.BadRequest;

            result = JsonConvert.SerializeObject(new { error = exception.Message });
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
