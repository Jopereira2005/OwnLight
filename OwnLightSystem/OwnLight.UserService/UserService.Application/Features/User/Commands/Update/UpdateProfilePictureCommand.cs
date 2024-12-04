using System.Text.Json.Serialization;
using MediatR;
using UserService.Application.Common.Services.Messages;

namespace UserService.Application.Features.User.Commands.Update;

public class UpdateProfilePictureCommand : IRequest<Message>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string ProfilePictureUrl { get; set; } = null!;
}
