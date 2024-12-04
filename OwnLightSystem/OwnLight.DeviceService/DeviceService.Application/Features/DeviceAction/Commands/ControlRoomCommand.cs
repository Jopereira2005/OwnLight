using System.Text.Json.Serialization;
using DeviceService.Domain.Enums;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Commands;

public class ControlRoomCommand : IRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public Guid RoomId { get; set; }
    public DeviceStatus Status { get; set; }
}
