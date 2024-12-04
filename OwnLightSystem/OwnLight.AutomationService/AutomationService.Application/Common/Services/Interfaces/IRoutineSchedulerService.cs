using AutomationService.Domain.Entities;

namespace AutomationService.Application.Common.Services.Interfaces;

public interface IRoutineSchedulerService
{
    Task ScheduleRoutineAsync(Routine routine);
    Task DeleteRoutineAsync(Guid routineId);
    Task UpdateRoutineAsync(Routine routine);
}
