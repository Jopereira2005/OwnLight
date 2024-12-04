using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = DeviceService.Domain.Entities;

namespace DeviceService.Application.Features.DeviceAction.Handlers.CommandHandlers;

public class DimmerizeDeviceCommandHandler(
    IDeviceService deviceService,
    IDeviceActionService deviceActionService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<DimmerizeDeviceCommand> validator
) : IRequestHandler<DimmerizeDeviceCommand>
{
    private readonly IDeviceService _deviceService = deviceService;
    private readonly IDeviceActionService _deviceActionService = deviceActionService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IValidator<DimmerizeDeviceCommand> _validator = validator;

    public async Task<Unit> Handle(
        DimmerizeDeviceCommand request,
        CancellationToken cancellationToken
    )
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        var userId =
            _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString()
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        var device = await _deviceService.GetDeviceByIdAsync(request.DeviceId, cancellationToken);

        if (device.UserId.ToString() != userId)
            throw new UnauthorizedAccessException(
                $"O dispositivo de id {request.DeviceId} não pertence ao usuário."
            );

        if (device.IsDimmable == false)
            throw new InvalidOperationException("O dispositivo não é dimmable.");

        var deviceAction = new Entity.DeviceAction
        {
            DeviceId = device.Id,
            UserId = Guid.Parse(userId),
            Action = request.Brightness == 0 ? DeviceActions.TurnOff : DeviceActions.Dim,
            Status = ActionStatus.Success,
        };

        await _deviceService.ControlDeviceBrightnessAsync(
            device.Id,
            request.Brightness,
            request.Brightness == 0 ? DeviceStatus.Off : DeviceStatus.On,
            cancellationToken
        );

        await _deviceActionService.LogDeviceActionAsync(deviceAction, cancellationToken);

        return Unit.Value;
    }
}
