using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Entity = DeviceService.Domain.Entities;

namespace DeviceService.Application.Features.DeviceAction.Handlers.CommandHandlers;

public class DimmerizeRoomCommandHandler(
    IDeviceService deviceService,
    IDeviceActionService deviceActionService,
    IHttpContextAccessor httpContextAccessor,
    IValidator<DimmerizeRoomCommand> validator
) : IRequestHandler<DimmerizeRoomCommand>
{
    private readonly IDeviceService _deviceService = deviceService;
    private readonly IDeviceActionService _deviceActionService = deviceActionService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IValidator<DimmerizeRoomCommand> _validator = validator;

    public async Task<Unit> Handle(
        DimmerizeRoomCommand request,
        CancellationToken cancellationToken
    )
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

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
                continue;

            var deviceAction = new Entity.DeviceAction
            {
                DeviceId = device.Id,
                UserId = Guid.Parse(userId),
                Action = request.Brightness == 0 ? DeviceActions.TurnOff : DeviceActions.Dim,
                Status = ActionStatus.Success,
            };

            actionsToLog.Add(deviceAction);
        }

        await _deviceService.ControlBrightnessByUserRoomAsync(
            Guid.Parse(userId),
            request.RoomId,
            request.Brightness,
            request.Brightness == 0 ? DeviceStatus.Off : DeviceStatus.On,
            cancellationToken
        );

        await _deviceActionService.LogDeviceActionsAsync(actionsToLog, cancellationToken);

        return Unit.Value;
    }
}
