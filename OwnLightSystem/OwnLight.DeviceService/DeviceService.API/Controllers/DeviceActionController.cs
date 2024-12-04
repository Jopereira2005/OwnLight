using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Application.Features.DeviceAction.Queries;
using DeviceService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceActionController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    [HttpPost("control/{deviceId}")]
    public async Task<ActionResult> ControlDeviceAsync(
        Guid deviceId,
        [FromBody] ControlDeviceCommand command
    )
    {
        command.DeviceId = deviceId;
        await _mediator.Send(command);
        return Ok("Dispositivo controlado com sucesso.");
    }

    [Authorize]
    [HttpPost("switch/{deviceId}")]
    public async Task<ActionResult> SwitchDeviceAsync(Guid deviceId)
    {
        var command = new SwitchDeviceCommand { DeviceId = deviceId };
        await _mediator.Send(command);
        return Ok("Dispositivo controlado com sucesso.");
    }

    [Authorize]
    [HttpPost("dim/{deviceId}")]
    public async Task<ActionResult> DimmerizeDeviceAsync(
        Guid deviceId,
        [FromBody] DimmerizeDeviceCommand command
    )
    {
        command.DeviceId = deviceId;
        await _mediator.Send(command);
        return Ok("Dispositivo controlado com sucesso.");
    }

    [Authorize]
    [HttpPost("control/room/{roomId}")]
    public async Task<ActionResult> ControlUserDevicesByRoomIdAsync(
        Guid roomId,
        [FromBody] ControlRoomCommand command
    )
    {
        command.RoomId = roomId;
        await _mediator.Send(command);
        return Ok("Dispositivos controlados com sucesso.");
    }

    [Authorize]
    [HttpPost("dim/room/{roomId}")]
    public async Task<ActionResult> DimmerizeUserDevicesByRoomIdAsync(
        Guid roomId,
        [FromBody] DimmerizeRoomCommand command
    )
    {
        command.RoomId = roomId;
        await _mediator.Send(command);
        return Ok("Dispositivos controlados com sucesso.");
    }

    [Authorize]
    [HttpPost("control/group/{groupId}")]
    public async Task<ActionResult> ControlUserDevicesByGroupIdAsync(
        Guid groupId,
        [FromBody] ControlGroupCommand command
    )
    {
        command.GroupId = groupId;
        await _mediator.Send(command);
        return Ok("Dispositivos controlados com sucesso.");
    }

    [Authorize]
    [HttpPost("dim/group/{groupId}")]
    public async Task<ActionResult> DimmerizeUserDevicesByGroupIdAsync(
        Guid groupId,
        [FromBody] DimmerizeGroupCommand command
    )
    {
        command.GroupId = groupId;
        await _mediator.Send(command);
        return Ok("Dispositivos controlados com sucesso.");
    }

    [Authorize]
    [HttpPost("control/all/user_devices")]
    public async Task<ActionResult> ControlAllUserDevicesAsync(
        [FromBody] ControlAllUserDevicesCommand command
    )
    {
        await _mediator.Send(command);
        return Ok("Dispositivos controlados com sucesso.");
    }

    [Authorize]
    [HttpGet("user_actions")]
    public async Task<ActionResult> GetUserActionsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetActionsByUserIdQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("device_actions/{deviceId}")]
    public async Task<ActionResult> GetDeviceActionsAsync(
        Guid deviceId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetActionsByDeviceIdQuery
        {
            DeviceId = deviceId,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("user_actions/status/{status}")]
    public async Task<ActionResult> GetUserActionsByStatusAsync(
        ActionStatus status,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetUserActionsByStatusQuery
        {
            Status = status.ToString(),
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("user_actions/type/{actionType}")]
    public async Task<ActionResult> GetUserActionsByTypeAsync(
        DeviceActions actionType,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetUserActionsByTypeQuery
        {
            Action = actionType.ToString(),
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("all/actions/type/{actionType}")]
    public async Task<ActionResult> GetActionsByTypeAsync(
        DeviceActions actionType,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetActionsByTypeQuery
        {
            Action = actionType.ToString(),
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("all/actions/status/{status}")]
    public async Task<ActionResult> GetActionsByStatusAsync(
        ActionStatus status,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetActionsByStatusQuery
        {
            Status = status.ToString(),
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
