using DeviceService.Domain.Entities;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using DeviceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Infrastructure.Repositories;

public class DeviceActionRepository(DataContext dataContext)
    : Repository<DeviceAction>(dataContext),
        IDeviceActionRepository
{
    private readonly DbSet<DeviceAction> _dbSet = dataContext.Set<DeviceAction>();

    public async Task AddDeviceActionsAsync(
        IEnumerable<DeviceAction> deviceActions,
        CancellationToken cancellationToken = default
    )
    {
        await _dbSet.AddRangeAsync(deviceActions, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<DeviceAction>> GetUserActionsAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Where(da => da.UserId == userId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DeviceAction>> GetActionsByDeviceIdAsync(
        Guid deviceId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Where(da => da.DeviceId == deviceId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DeviceAction>> GetUserActionsByStatusAsync(
        Guid userId,
        ActionStatus actionStatus,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Where(da => da.UserId == userId && da.Status == actionStatus)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DeviceAction>> GetUserActionsByTypeAsync(
        Guid userId,
        DeviceActions actionType,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Where(da => da.UserId == userId && da.Action == actionType)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DeviceAction>> GetActionsByTypeAsync(
        DeviceActions actionType,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Where(da => da.Action == actionType)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DeviceAction>> GetActionsByStatusAsync(
        ActionStatus actionStatus,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Where(da => da.Status == actionStatus)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
