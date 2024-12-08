using DeviceService.Application.Features.Device.Commands;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.Device.Handlers.CommandHandlers;

public class DeleteDevicesByRoomCommandHandler(IDeviceRepository deviceRepository)
    : IRequestHandler<DeleteDevicesByRoomCommand>
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;

    public async Task<Unit> Handle(
        DeleteDevicesByRoomCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.RoomId == Guid.Empty)
            throw new ArgumentException("Room ID cannot be empty.");

        await _deviceRepository.DeleteByRoomIdAsync(request.RoomId, cancellationToken);

        return Unit.Value;
    }
}
