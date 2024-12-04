using AutomationService.Domain.Enums;

namespace AutomationService.Application.Contracts.DTOs;

public class RoutineLogDTO
{
    public Guid Id { get; set; }
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
    public Guid TargetId { get; set; }
    public required string ActionTarget { get; set; }
    public required string ActionStatus { get; set; }
    public required string ActionType { get; set; }
    public required string ErrorMessage { get; set; }
    public DateTime ExecutedAt { get; set; }
}
