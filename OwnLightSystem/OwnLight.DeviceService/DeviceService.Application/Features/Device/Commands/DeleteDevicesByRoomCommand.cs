using MediatR;

namespace DeviceService.Application.Features.Device.Commands;

public class DeleteDevicesByRoomCommand(Guid roomId) : IRequest
{
    public Guid RoomId { get; set; } = roomId;
}
