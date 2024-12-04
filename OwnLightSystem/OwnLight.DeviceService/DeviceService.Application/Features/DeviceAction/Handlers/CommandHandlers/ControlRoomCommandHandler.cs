using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = DeviceService.Domain.Entities;

namespace DeviceService.Application.Features.DeviceAction.Handlers.CommandHandlers;

public class ControlRoomCommandHandler(
    IDeviceService deviceService,
    IDeviceActionService deviceActionService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<ControlRoomCommand> validator
) : IRequestHandler<ControlRoomCommand>
{
    private readonly IDeviceService _deviceService = deviceService;
    private readonly IDeviceActionService _deviceActionService = deviceActionService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IValidator<ControlRoomCommand> _validator = validator;

    public async Task<Unit> Handle(ControlRoomCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        var userId =
            _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString()
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        var devices = await _deviceService.GetUserDevicesByRoomIdAsync(
            Guid.Parse(userId),
            request.RoomId,
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
                Status = ActionStatus.Success,
            };

            actionsToLog.Add(deviceAction);
        }

        await _deviceService.ControlUserDevicesByRoomIdAsync(
            Guid.Parse(userId),
            request.RoomId,
            request.Status,
            cancellationToken
        );
        await _deviceActionService.LogDeviceActionsAsync(actionsToLog, cancellationToken);

        return Unit.Value;
    }
}
