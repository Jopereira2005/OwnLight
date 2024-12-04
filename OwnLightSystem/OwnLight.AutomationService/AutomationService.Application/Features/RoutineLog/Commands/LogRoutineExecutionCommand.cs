using AutomationService.Domain.Enums;
using MediatR;

namespace AutomationService.Application.Features.RoutineLog.Commands;

public class LogRoutineExecutionCommand(
    Guid routineId,
    Guid userId,
    Guid targetId,
    RoutineActionType actionType,
    ActionStatus actionStatus,
    string? errorMessage
) : IRequest
{
    public Guid RoutineId { get; set; } = routineId;
    public Guid UserId { get; set; } = userId;
    public Guid TargetId { get; set; } = targetId;
    public RoutineActionType ActionType { get; set; } = actionType;
    public ActionStatus ActionStatus { get; set; } = actionStatus;
    public string? ErrorMessage { get; set; } = errorMessage;
}
