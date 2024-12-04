using AutomationService.Domain.Entities;
using AutomationService.Domain.Enums;
using AutomationService.Domain.Interfaces;
using AutomationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomationService.Infrastructure.Repositories;

public class RoutineExecutionLogRepository(DataContext dataContext)
    : Repository<RoutineExecutionLog>(dataContext),
        IRoutineExecutionLogRepository
{
    private readonly DbSet<RoutineExecutionLog> _dbSet = dataContext.Set<RoutineExecutionLog>();

    public async Task<IEnumerable<RoutineExecutionLog>> GetByActionStatus(
        ActionStatus actionStatus,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (page - 1) * pageSize;
        return await _dbSet
            .Where(r => r.ActionStatus == actionStatus)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RoutineExecutionLog>> GetByUserId(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (page - 1) * pageSize;
        return await _dbSet
            .Where(r => r.UserId == userId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RoutineExecutionLog>> GetByRoutineId(
        Guid routineId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (page - 1) * pageSize;
        return await _dbSet
            .Where(r => r.RoutineId == routineId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RoutineExecutionLog>> GetByTaretId(
        Guid targetId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (page - 1) * pageSize;
        return await _dbSet
            .Where(r => r.TargetId == targetId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
