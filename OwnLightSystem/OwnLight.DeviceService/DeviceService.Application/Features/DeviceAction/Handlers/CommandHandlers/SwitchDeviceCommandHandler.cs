using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = DeviceService.Domain.Entities;

namespace DeviceService.Application.Features.DeviceAction.Handlers.CommandHandlers;

public class SwitchDeviceCommandHandler(
    IDeviceService deviceService,
    IDeviceActionService deviceActionService,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<SwitchDeviceCommand>
{
    private readonly IDeviceService _deviceService = deviceService;
    private readonly IDeviceActionService _deviceActionService = deviceActionService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Unit> Handle(SwitchDeviceCommand request, CancellationToken cancellationToken)
    {
        var userId =
            _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString()
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        var device = await _deviceService.GetDeviceByIdAsync(request.DeviceId, cancellationToken);

        if (device.UserId.ToString() != userId)
            throw new UnauthorizedAccessException(
                $"O dispositivo de id {request.DeviceId} não pertence ao usuário."
            );

        if (device.Status == DeviceStatus.On)
        {
            await _deviceService.SwitchDeviceAsync(
                request.DeviceId,
                brightness: 0,
                DeviceStatus.Off,
                cancellationToken
            );
        }
        else
        {
            await _deviceService.SwitchDeviceAsync(
                request.DeviceId,
                brightness: 100,
                DeviceStatus.On,
                cancellationToken
            );
        }

        var deviceAction = new Entity.DeviceAction
        {
            DeviceId = request.DeviceId,
            Action =
                device.Status == DeviceStatus.On ? DeviceActions.TurnOff : DeviceActions.TurnOn,
            UserId = Guid.Parse(userId),
        };

        await _deviceActionService.LogDeviceActionAsync(deviceAction, cancellationToken);

        return Unit.Value;
    }
}
