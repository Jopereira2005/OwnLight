using System.Text.Json.Serialization;
using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.User.Commands.Update;

public class UpdateUsernameCommand : IRequest<Message>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public required string Username { get; set; }
}
