using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = DeviceService.Domain.Entities;

namespace DeviceService.Application.Features.DeviceAction.Handlers.CommandHandlers;

public class ControlDeviceCommandHandler(
    IDeviceService deviceService,
    IDeviceActionService deviceActionService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<ControlDeviceCommand> validator
) : IRequestHandler<ControlDeviceCommand>
{
    private readonly IDeviceService _deviceService = deviceService;
    private readonly IDeviceActionService _deviceActionService = deviceActionService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IValidator<ControlDeviceCommand> _validator = validator;

    public async Task<Unit> Handle(
        ControlDeviceCommand request,
        CancellationToken cancellationToken
    )
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var userId =
            _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString()
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        var device = await _deviceService.GetDeviceByIdAsync(request.DeviceId, cancellationToken);

        if (device.UserId != Guid.Parse(userId))
            throw new UnauthorizedAccessException(
                $"O dispositivo de id {request.DeviceId} não pertence ao usuário."
            );

        if (device.IsDimmable == false)
            device.Brightness = null;
        else
            device.Brightness = request.Status == DeviceStatus.On ? 100 : 0;

        var deviceAction = new Entity.DeviceAction
        {
            DeviceId = request.DeviceId,
            UserId = Guid.Parse(userId),
            Action =
                request.Status == DeviceStatus.On ? DeviceActions.TurnOn : DeviceActions.TurnOff,
        };

        await _deviceService.ControlDeviceAsync(
            request.DeviceId,
            request.Status,
            cancellationToken
        );

        await _deviceActionService.LogDeviceActionAsync(deviceAction, cancellationToken);

        return Unit.Value;
    }
}
