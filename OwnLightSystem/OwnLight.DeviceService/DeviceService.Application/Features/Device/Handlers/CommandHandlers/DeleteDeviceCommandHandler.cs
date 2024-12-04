using DeviceService.Application.Features.Device.Commands;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.Device.Handlers.CommandHandlers;

public class DeleteDeviceCommandHandler(IDeviceRepository deviceRepository)
    : IRequestHandler<DeleteDeviceCommand>
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;

    public async Task<Unit> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var device =
            await _deviceRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Device with ID {request.Id} not found.");

        await _deviceRepository.DeleteAsync(device.Id, cancellationToken);

        return Unit.Value;
    }
}
