using MediatR;
using Entity = AutomationService.Domain.Entities;

namespace AutomationService.Application.Features.Routine.Queries;

public class GetRoutineByIdQuery(Guid id) : IRequest<Entity.Routine>
{
    public Guid Id { get; set; } = id;
}
