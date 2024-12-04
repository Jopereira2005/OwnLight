using AutomationService.Application.Contracts.DTOs;
using AutomationService.Application.Features.RoutineLog.Queries;
using AutomationService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomationService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutineLogController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    [HttpGet]
    [Route("get/user_logs")]
    public async Task<ActionResult<PaginatedResultDTO<RoutineLogDTO>>> GetRoutineLogsByUserId(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetLogsByUserIdQuery(pageNumber, pageSize);
        return Ok(await _mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    [Route("get/by_routine/{routineId}")]
    public async Task<ActionResult<PaginatedResultDTO<RoutineLogDTO>>> GetRoutineLogsByRoutineId(
        Guid routineId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetLogsByRoutineIdQuery(routineId, pageNumber, pageSize);
        return Ok(await _mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    [Route("get/by_status/{actionStatus}")]
    public async Task<ActionResult<PaginatedResultDTO<RoutineLogDTO>>> GetRoutineLogsByActionStatus(
        ActionStatus actionStatus,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetLogsByActionStatusQuery(actionStatus, pageNumber, pageSize);
        return Ok(await _mediator.Send(query));
    }

    [Authorize]
    [HttpGet]
    [Route("get/by_target/{targetId}")]
    public async Task<ActionResult<PaginatedResultDTO<RoutineLogDTO>>> GetRoutineLogsByTargetId(
        Guid targetId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetLogsByTargetIdQuery(targetId, pageNumber, pageSize);
        return Ok(await _mediator.Send(query));
    }
}
