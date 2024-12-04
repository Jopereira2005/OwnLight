using System.Text.Json.Serialization;
using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.User.Commands.Update;

public class UpdateEmailCommand : IRequest<Message>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public required string Email { get; set; }
}
