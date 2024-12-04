using System.Text.Json.Serialization;
using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.Authentication.Command;

public class LogoutCommand(Guid id) : IRequest<Message>
{
    [JsonIgnore]
    public Guid Id { get; set; } = id;
}
