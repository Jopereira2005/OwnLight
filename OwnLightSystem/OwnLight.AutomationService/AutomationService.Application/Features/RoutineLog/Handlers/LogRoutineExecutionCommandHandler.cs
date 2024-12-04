using AutomationService.Application.Features.RoutineLog.Commands;
using AutomationService.Domain.Entities;
using AutomationService.Domain.Interfaces;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Handlers;

public class LogRoutineExecutionCommandHandler(
    IRoutineExecutionLogRepository routineExecutionLogRepository
) : IRequestHandler<LogRoutineExecutionCommand>
{
    private readonly IRoutineExecutionLogRepository _routineExecutionLogRepository =
        routineExecutionLogRepository;

    public async Task<Unit> Handle(
        LogRoutineExecutionCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var executionLog = new RoutineExecutionLog
            {
                RoutineId = request.RoutineId,
                UserId = request.UserId,
                TargetId = request.TargetId,
                ActionType = request.ActionType,
                ActionStatus = request.ActionStatus,
                ErrorMessage = request.ErrorMessage ?? string.Empty,
                ExecutedAt = DateTime.UtcNow,
            };

            await _routineExecutionLogRepository.CreateAsync(executionLog, cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao registrar a execução da rotina.", ex);
        }
    }
}
