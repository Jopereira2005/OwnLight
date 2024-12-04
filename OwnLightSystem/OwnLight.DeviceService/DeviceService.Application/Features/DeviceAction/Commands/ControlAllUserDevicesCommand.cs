using System.Text.Json.Serialization;
using DeviceService.Domain.Enums;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Commands;

public class ControlAllUserDevicesCommand : IRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public DeviceStatus Status { get; set; }
}
