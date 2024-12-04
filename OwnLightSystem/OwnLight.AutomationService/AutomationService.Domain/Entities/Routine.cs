using AutomationService.Domain.Enums;

namespace AutomationService.Domain.Entities;

public class Routine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public TimeSpan ExecutionTime { get; set; }
    public RoutineActionType ActionType { get; set; }
    public bool IsOneTime { get; set; }
    public bool IsCustom { get; set; }
    public bool IsActive { get; set; }
    public List<DayOfWeek> DaysOfWeek { get; set; } = new List<DayOfWeek>();

    public Guid? TargetId { get; set; }
    public int? Brightness { get; set; }
    public ActionTarget ActionTarget { get; set; }

    public ICollection<RoutineExecutionLog> ExecutionLogs { get; set; } =
        new List<RoutineExecutionLog>();
}
