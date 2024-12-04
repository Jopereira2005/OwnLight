using System.Text.Json.Serialization;
using DeviceService.Domain.Enums;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Commands;

public class ControlGroupCommand : IRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public Guid GroupId { get; set; }
    public DeviceStatus Status { get; set; }
}
