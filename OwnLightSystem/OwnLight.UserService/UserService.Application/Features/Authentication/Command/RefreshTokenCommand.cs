using System.Text.Json.Serialization;
using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.Authentication.Command;

public class RefreshTokenCommand(Guid userId) : IRequest<Message>
{
    [JsonIgnore]
    public Guid UserId { get; set; } = userId;
}
