using AutomationService.Application.Features.Routine.Queries;
using AutomationService.Domain.Interfaces;
using MediatR;
using Entity = AutomationService.Domain.Entities;

namespace AutomationService.Application.Features.Routine.Handlers.Queries;

public class GetRoutineByIdQueryHandler(IRoutineRepository routineRepository)
    : IRequestHandler<GetRoutineByIdQuery, Entity.Routine>
{
    private readonly IRoutineRepository _routineRepository = routineRepository;

    public async Task<Entity.Routine> Handle(
        GetRoutineByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var routine =
            await _routineRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Rotina com ID {request.Id} não encontrada.");

        return routine;
    }
}
