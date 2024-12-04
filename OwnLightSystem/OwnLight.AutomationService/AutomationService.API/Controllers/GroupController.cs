using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Group.Commands;
using AutomationService.Application.Features.Group.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomationService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Guid>> CreateGroup([FromBody] CreateGroupCommand command) =>
        Ok(await _mediator.Send(command));

    [Authorize]
    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] UpdateGroupCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return Ok("Grupo atualizado com sucesso.");
    }

    [Authorize]
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteGroup(Guid id)
    {
        await _mediator.Send(new DeleteGroupCommand { Id = id });
        return Ok("Grupo excluído com sucesso.");
    }

    [Authorize]
    [HttpPost]
    [Route("add_devices/{groupId}")]
    public async Task<IActionResult> AddDevicesToGroup(
        Guid groupId,
        [FromBody] AddGroupDevicesCommand command
    )
    {
        command.GroupId = groupId;
        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpDelete]
    [Route("remove_devices/{groupId}")]
    public async Task<IActionResult> RemoveDevicesFromGroup(
        Guid groupId,
        [FromBody] RemoveGroupDevicesCommand command
    )
    {
        command.GroupId = groupId;
        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpGet]
    [Route("get/user_groups")]
    public async Task<ActionResult<PaginatedResultDTO<GroupResponseDTO>>> GetUserGroups(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    ) => Ok(await _mediator.Send(new GetUserGroupsQuery(pageNumber, pageSize)));

    [Authorize]
    [HttpGet]
    [Route("get/user_group/{groupName}")]
    public async Task<ActionResult<GroupResponseDTO>> GetUserGroup(string groupName) =>
        Ok(await _mediator.Send(new GetUserGroupByNameQuery(groupName)));

    [Authorize]
    [HttpGet]
    [Route("get/group_devices/{groupId}")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetGroupDevices(Guid groupId) =>
        Ok(await _mediator.Send(new GetGroupDevicesQuery(groupId)));
}
