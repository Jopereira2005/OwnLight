using MediatR;

namespace AutomationService.Application.Features.Routine.Commands;

public class SwitchRoutineStatusCommand(Guid id) : IRequest
{
    public Guid Id { get; set; } = id;
}
