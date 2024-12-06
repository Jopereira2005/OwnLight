using Microsoft.AspNetCore.Http;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Common.Services.Messages;

public class MessageService : IMessageService
{
    public Message CreateValidationMessage(IEnumerable<string> errors)
    {
        return Message.Error(
            "Validation Error",
            string.Join(", ", errors),
            "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            StatusCodes.Status400BadRequest.ToString()
        );
    }

    public Message CreateNotAuthorizedMessage(string message)
    {
        return Message.Error(
            "Not Authorized",
            message,
            "https://tools.ietf.org/html/rfc7235#section-3.1",
            StatusCodes.Status401Unauthorized.ToString()
        );
    }

    public Message CreateConflictMessage(string message)
    {
        return Message.Error(
            "Conflict",
            message,
            "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            StatusCodes.Status409Conflict.ToString()
        );
    }

    public Message CreateNotFoundMessage(string message)
    {
        return Message.Error(
            "Not Found",
            message,
            "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            StatusCodes.Status404NotFound.ToString()
        );
    }

    public Message CreateSuccessMessage(string message)
    {
        return Message.Success(
            "Success",
            message,
            "https://tools.ietf.org/html/rfc7231#section-6.3.1",
            StatusCodes.Status200OK.ToString()
        );
    }

    public Message CreateBadRequestMessage(string message)
    {
        return Message.Error(
            "Bad Request",
            message,
            "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            StatusCodes.Status400BadRequest.ToString()
        );
    }

    public Message CreateInternalErrorMessage(string message)
    {
        return Message.Error(
            "Internal Server Error",
            message,
            "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            StatusCodes.Status500InternalServerError.ToString()
        );
    }

    public Message CreateCreatedMessage(string message)
    {
        return Message.Success(
            "Created",
            message,
            "https://tools.ietf.org/html/rfc7231#section-6.3.2",
            StatusCodes.Status201Created.ToString()
        );
    }

    public Message CreateLoginMessage(string message, string accessToken, object userData)
    {
        return Message.LogedIn(
            "Login realizado com sucesso.",
            message,
            "Success",
            StatusCodes.Status200OK.ToString(),
            accessToken,
            userData
        );
    }

    public Message CreateRefreshMessage(string message, string accessToken)
    {
        return Message.RefreshToken(
            "Login realizado com sucesso.",
            message,
            "Success",
            StatusCodes.Status200OK.ToString(),
            accessToken
        );
    }
}
