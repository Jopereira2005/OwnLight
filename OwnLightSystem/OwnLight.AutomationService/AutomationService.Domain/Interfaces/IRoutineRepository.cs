using AutomationService.Domain.Entities;

namespace AutomationService.Domain.Interfaces;

public interface IRoutineRepository : IRepository<Routine>
{
    public Task<IEnumerable<Routine>> GetUserRoutinesAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    );
    public Task<Routine?> GetRoutineByNameAsync(
        string routineName,
        CancellationToken cancellationToken = default
    );
}
