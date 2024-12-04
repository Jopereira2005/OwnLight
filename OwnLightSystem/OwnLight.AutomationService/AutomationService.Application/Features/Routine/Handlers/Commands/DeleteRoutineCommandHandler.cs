using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Application.Features.Routine.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Routine.Handlers.Commands;

public class DeleteRoutineCommandHandler(
    IRoutineRepository routineRepository,
    IRoutineSchedulerService schedulerFactory
) : IRequestHandler<DeleteRoutineCommand>
{
    private readonly IRoutineRepository _routineRepository = routineRepository;
    private readonly IRoutineSchedulerService _schedulerFactory = schedulerFactory;

    public async Task<Unit> Handle(
        DeleteRoutineCommand request,
        CancellationToken cancellationToken
    )
    {
        var routine =
            await _routineRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Rotina não encontrada.");

        await _routineRepository.DeleteAsync(routine.Id, cancellationToken);
        await _schedulerFactory.DeleteRoutineAsync(routine.Id);

        return Unit.Value;
    }
}
