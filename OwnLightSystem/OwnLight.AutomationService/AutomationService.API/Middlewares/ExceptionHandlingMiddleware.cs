using System.Net;
using AutoMapper;
using FluentValidation;
using Newtonsoft.Json;

namespace AutomationService.API.Middlewares;

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

            result = JsonConvert.SerializeObject(new { error = "Validation failed", errors });
        }
        else if (exception is AutoMapperConfigurationException)
        {
            code = HttpStatusCode.InternalServerError;
            result = JsonConvert.SerializeObject(new { error = "AutoMapper configuration error" });
        }
        else if (exception is AutoMapperMappingException autoMapperMappingException)
        {
            code = HttpStatusCode.InternalServerError;
            result = JsonConvert.SerializeObject(
                new
                {
                    error = "AutoMapper mapping error",
                    details = CleanExceptionMessage(autoMapperMappingException.Message),
                    innerException = autoMapperMappingException.InnerException?.Message,
                }
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

    private static string CleanExceptionMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
            return string.Empty;

        return message.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ").Trim();
    }
}
