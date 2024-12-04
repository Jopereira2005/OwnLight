using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = DeviceService.Domain.Entities;

namespace DeviceService.Application.Features.DeviceAction.Handlers.CommandHandlers;

public class ControlAllUserDevicesCommandHandler(
    IDeviceService deviceService,
    IDeviceActionService deviceActionService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<ControlAllUserDevicesCommand> validator
) : IRequestHandler<ControlAllUserDevicesCommand>
{
    private readonly IDeviceService _deviceService = deviceService;
    private readonly IDeviceActionService _deviceActionService = deviceActionService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IValidator<ControlAllUserDevicesCommand> _validator = validator;

    public async Task<Unit> Handle(
        ControlAllUserDevicesCommand request,
        CancellationToken cancellationToken
    )
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var userId =
            _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString()
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        var devices = await _deviceService.GetUserDevicesAsync(
            Guid.Parse(userId),
            pageNumber: 1,
            pageSize: 30,
            cancellationToken
        );
        var actionsToLog = new List<Entity.DeviceAction>();

        foreach (var device in devices)
        {
            if (device.UserId != Guid.Parse(userId))
                throw new UnauthorizedAccessException(
                    $"O dispositivo de id {device.Id} não pertence ao usuário."
                );

            if (device.IsDimmable == false)
                device.Brightness = null;
            else
                device.Brightness = request.Status == DeviceStatus.On ? 100 : 0;

            var deviceAction = new Entity.DeviceAction
            {
                DeviceId = device.Id,
                UserId = Guid.Parse(userId),
                Action =
                    request.Status == DeviceStatus.On
                        ? DeviceActions.TurnOn
                        : DeviceActions.TurnOff,
            };

            actionsToLog.Add(deviceAction);
        }
        await _deviceService.ControlAllUserDevicesAsync(
            Guid.Parse(userId),
            request.Status,
            cancellationToken
        );

        await _deviceActionService.LogDeviceActionsAsync(actionsToLog, cancellationToken);

        return Unit.Value;
    }
}
