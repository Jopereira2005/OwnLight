using System.ComponentModel;
using System.Text.Json.Serialization;
using MediatR;

namespace AutomationService.Application.Features.Room.Commands;

public class UpdateRoomCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [DefaultValue("Room")]
    public required string Name { get; set; }

    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
