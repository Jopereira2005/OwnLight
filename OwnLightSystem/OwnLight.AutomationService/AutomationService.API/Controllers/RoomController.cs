using AutomationService.Application.Features.Room.Commands;
using AutomationService.Application.Features.Room.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomationService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command) =>
        Ok(await _mediator.Send(command));

    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] UpdateRoomCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return Ok($"Cômodo '{command.Name}' atualizado com sucesso.");
    }

    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRoom(Guid id)
    {
        await _mediator.Send(new DeleteRoomCommand { Id = id });
        return Ok("Cômodo removido com sucesso.");
    }

    [Authorize]
    [HttpPost("add_devices/{groupId}")]
    public async Task<IActionResult> AddRoomDevices(
        Guid groupId,
        [FromBody] AddRoomDevicesCommand command
    )
    {
        command.GroupId = groupId;
        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpDelete("remove_devices/{groupId}")]
    public async Task<IActionResult> RemoveRoomDevices(
        Guid groupId,
        [FromBody] RemoveRoomDevicesCommand command
    )
    {
        command.GroupId = groupId;
        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpGet("get/user_rooms")]
    public async Task<IActionResult> GetUserRooms(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    ) => Ok(await _mediator.Send(new GetUserRoomsQuery(pageNumber, pageSize)));

    [Authorize]
    [HttpGet("get/room_devices/{roomId}")]
    public async Task<IActionResult> GetRoomDevices(Guid roomId) =>
        Ok(await _mediator.Send(new GetRoomDevicesQuery(roomId)));

    [Authorize]
    [HttpGet("get/user_room/{roomName}")]
    public async Task<IActionResult> GetUserRoomByName(string roomName) =>
        Ok(await _mediator.Send(new GetUserRoomByNameQuery(roomName)));
}
