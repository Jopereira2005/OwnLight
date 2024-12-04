using MediatR;

namespace AutomationService.Application.Features.Routine.Commands;

public class DeleteRoutineCommand : IRequest
{
    public Guid Id { get; set; }
}
