using MediatR;

namespace DeviceService.Application.Features.DeviceType.Commands;

public class DeleteDeviceTypeCommand : IRequest
{
    public Guid Id { get; set; }
}
