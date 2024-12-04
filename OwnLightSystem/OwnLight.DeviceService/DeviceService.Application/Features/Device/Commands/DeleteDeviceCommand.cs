using MediatR;

namespace DeviceService.Application.Features.Device.Commands;

public class DeleteDeviceCommand(Guid id) : IRequest
{
    public Guid Id { get; set; } = id;
}
