using MediatR;

namespace DeviceService.Application.Features.DeviceAction.Commands;

public class SwitchDeviceCommand : IRequest
{
    public Guid DeviceId { get; set; }
}
