using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Application.Features.Routine.Commands;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.Routine.Handlers.Commands;

public class SwitchRoutineStatusCommandHandler(
    IRoutineRepository routineRepository,
    IRoutineSchedulerService schedulerService
) : IRequestHandler<SwitchRoutineStatusCommand>
{
    private readonly IRoutineRepository _routineRepository = routineRepository;
    private readonly IRoutineSchedulerService _schedulerService = schedulerService;

    public async Task<Unit> Handle(
        SwitchRoutineStatusCommand request,
        CancellationToken cancellationToken
    )
    {
        var routine =
            await _routineRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Rotina não encontrada.");

        routine.IsActive = !routine.IsActive;
        await _routineRepository.UpdateAsync(routine, cancellationToken);

        if (routine.IsActive)
            await _schedulerService.ScheduleRoutineAsync(routine);
        else
            await _schedulerService.DeleteRoutineAsync(routine.Id);

        return Unit.Value;
    }
}
