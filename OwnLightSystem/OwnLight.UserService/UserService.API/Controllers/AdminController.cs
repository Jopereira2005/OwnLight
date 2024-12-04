using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Features.Admin.Commands;
using UserService.Infrastructure.HostedServices;

namespace UserService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController(IMediator mediator, TokenCleanupStateService tokenCleanupStateService)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly TokenCleanupStateService _tokenCleanupStateService = tokenCleanupStateService;

    [HttpDelete("delete/all")]
    public async Task<ActionResult> DeleteAll(
        [FromQuery] Guid adminId,
        [FromQuery] string adminPassword
    )
    {
        var command = new DeleteAllCommand { AdminId = adminId, AdminPassword = adminPassword };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("token-cleanup/last-run")]
    public ActionResult<string> GetLastTokenCleanupRun()
    {
        var lastRun = _tokenCleanupStateService.GetLastRun();
        if (lastRun.HasValue)
        {
            var formattedLastRun = lastRun.Value.ToString("yyyy-MM-dTHH:mm");
            return Ok(formattedLastRun);
        }

        return Ok("Nenhuma limpaza de token foi realizada ainda");
    }
}
