using DeviceService.Application.Features.DeviceType.Commands;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.DeviceType.Handlers.CommandHandlers;

public class DeleteDeviceTypeCommandHandler(IDeviceTypeRepository deviceTypeRepository)
    : IRequestHandler<DeleteDeviceTypeCommand>
{
    private readonly IDeviceTypeRepository _deviceTypeRepository = deviceTypeRepository;

    public async Task<Unit> Handle(
        DeleteDeviceTypeCommand request,
        CancellationToken cancellationToken
    )
    {
        var deviceType =
            await _deviceTypeRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"DeviceType with ID {request.Id} not found.");
        await _deviceTypeRepository.DeleteAsync(deviceType.Id, cancellationToken);

        return Unit.Value;
    }
}
