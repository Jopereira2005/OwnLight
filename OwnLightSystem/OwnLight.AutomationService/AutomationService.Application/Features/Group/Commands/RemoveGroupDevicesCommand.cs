using System.Text.Json.Serialization;
using MediatR;

namespace AutomationService.Application.Features.Group.Commands;

public class RemoveGroupDevicesCommand(Guid groupId, Guid[] deviceIds) : IRequest<string>
{
    [JsonIgnore]
    public Guid GroupId { get; set; } = groupId;
    public Guid[] DeviceIds { get; set; } = deviceIds;
}
