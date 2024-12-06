namespace UserService.Application.Common.Services.Messages;

public interface IMessageService
{
    Message CreateValidationMessage(IEnumerable<string> errors);
    Message CreateCreatedMessage(string message);
    Message CreateNotAuthorizedMessage(string message);
    Message CreateBadRequestMessage(string message);
    Message CreateConflictMessage(string message);
    Message CreateNotFoundMessage(string message);
    Message CreateSuccessMessage(string message);
    Message CreateInternalErrorMessage(string message);
    Message CreateLoginMessage(string message, string accessToken, object userData);
    Message CreateRefreshMessage(string message, string accessToken);
}
