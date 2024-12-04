using System.Text.Json.Serialization;
using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Commands;

public class DimmerizeRoomCommand : IRequest
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public Guid RoomId { get; set; }
    public int Brightness { get; set; }
}
