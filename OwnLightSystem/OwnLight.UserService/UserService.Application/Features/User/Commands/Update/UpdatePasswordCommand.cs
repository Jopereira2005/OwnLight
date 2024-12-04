using System.Text.Json.Serialization;
using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.User.Commands.Update;

public class UpdatePasswordCommand : IRequest<Message>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
    public required string NewPassword { get; set; }
    public required string CurrentPassword { get; set; }
}
