using System.Text.Json.Serialization;
using MediatR;

namespace AutomationService.Application.Features.Room.Commands;

public class RemoveRoomDevicesCommand(Guid groupId, Guid[] deviceIds) : IRequest<string>
{
    [JsonIgnore]
    public Guid GroupId { get; set; } = groupId;
    public Guid[] DeviceIds { get; set; } = deviceIds;
}
