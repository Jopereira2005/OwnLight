namespace AutomationService.Application.Contracts.DTOs;

public class RoutineResponseDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public required string ActionType { get; set; }

    public Guid TargetId { get; set; }
    public required string ActionTarget { get; set; }
    public int? Brightness { get; set; }
}
