using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.Routine.Commands;
using AutomationService.Application.Features.Routine.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutineController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateRoutine([FromBody] CreateRoutineCommand command)
    {
        await _mediator.Send(command);
        return Ok("Rotina criada com sucesso.");
    }

    [Authorize]
    [HttpPut("switch_status/{Id}")]
    public async Task<IActionResult> SwitchRoutineStatus(Guid Id)
    {
        var command = new SwitchRoutineStatusCommand(Id);
        await _mediator.Send(command);
        return Ok("Status da rotina alterado com sucesso.");
    }

    [Authorize]
    [HttpPut]
    [Route("update/{Id}")]
    public async Task<IActionResult> UpdateRoutine(Guid Id, [FromBody] UpdateRoutineCommand command)
    {
        command.Id = Id;
        await _mediator.Send(command);
        return Ok("Rotina atualizada com sucesso.");
    }

    [Authorize]
    [HttpPut]
    [Route("update/name/{Id}")]
    public async Task<IActionResult> UpdateRoutine(
        Guid Id,
        [FromBody] UpdateRoutineNameCommand command
    )
    {
        command.Id = Id;
        await _mediator.Send(command);
        return Ok("Rotina atualizada com sucesso. Novo nome: " + command.Name);
    }

    [Authorize]
    [HttpDelete]
    [Route("delete/{Id}")]
    public async Task<IActionResult> DeleteRoutine(Guid Id)
    {
        var command = new DeleteRoutineCommand(Id);
        await _mediator.Send(command);
        return Ok("Rotina deletada com sucesso.");
    }

    [Authorize]
    [HttpGet]
    [Route("get/by_name/{name}")]
    public async Task<ActionResult<RoutineResponseDTO>> GetRoutineByName(string name)
    {
        var query = new GetRoutineByNameQuery { Name = name };
        return Ok(await _mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    [Route("get/user_routines")]
    public async Task<ActionResult<PaginatedResultDTO<RoutineResponseDTO>>> GetAllUserRoutines(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetAllUserRoutinesQuery(pageNumber, pageSize);
        return Ok(await _mediator.Send(query));
    }
}
