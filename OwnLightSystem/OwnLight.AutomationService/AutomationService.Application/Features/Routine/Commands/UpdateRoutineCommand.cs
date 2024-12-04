using System.Text.Json.Serialization;
using AutomationService.Domain.Enums;
using MediatR;

namespace AutomationService.Application.Features.Routine.Commands;

public class UpdateRoutineCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public RoutineActionType ActionType { get; set; }
    public int? Brightness { get; set; }
    public bool IsOneTime { get; set; }
    public bool IsCustom { get; set; }
    public bool IsActive { get; set; }
    public List<DayOfWeek>? DaysOfWeek { get; set; }
}
