using AutomationService.Domain.Enums;

namespace AutomationService.Domain.Entities;

public class RoutineExecutionLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid TargetId { get; set; }

    public Guid RoutineId { get; set; }
    public Routine Routine { get; set; } = null!;

    public ActionTarget ActionTarget { get; set; }
    public ActionStatus ActionStatus { get; set; }
    public RoutineActionType ActionType { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
}
