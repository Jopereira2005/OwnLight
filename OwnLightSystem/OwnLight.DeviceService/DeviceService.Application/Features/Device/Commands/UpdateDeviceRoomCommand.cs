using System.Text.Json.Serialization;
using MediatR;

namespace DeviceService.Application.Features.Device.Commands;

public class UpdateDeviceRoomCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
