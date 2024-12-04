using AutomationService.Domain.Entities;
using AutomationService.Domain.Interfaces;
using AutomationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomationService.Infrastructure.Repositories;

public class GroupRepository(DataContext dataContext)
    : Repository<Group>(dataContext),
        IGroupRepository
{
    private readonly DbSet<Group> _dbSet = dataContext.Set<Group>();

    public async Task<IEnumerable<Group>> GetUserGroupsAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (page - 1) * pageSize;
        return await _dbSet
            .Where(g => g.UserId == userId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Group?> GetUserGroupByNameAsync(
        Guid userId,
        string groupName,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet
            .Where(g => g.UserId == userId && g.Name == groupName)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddDevicesToGroupAsync(
        Guid groupId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    )
    {
        var group = await _dbSet.FindAsync([groupId], cancellationToken);
        var currentDeviceIds = string.IsNullOrEmpty(group!.DeviceIds)
            ? []
            : group.DeviceIds.Split(',').Select(Guid.Parse).ToList();

        foreach (var deviceId in deviceIds)
            if (!currentDeviceIds.Contains(deviceId))
                currentDeviceIds.Add(deviceId);

        group.DeviceIds = string.Join(',', currentDeviceIds);
        group.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveDevicesFromGroupAsync(
        Guid groupId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    )
    {
        var group = await _dbSet.FindAsync([groupId], cancellationToken);
        var currentDeviceIds = string.IsNullOrEmpty(group!.DeviceIds)
            ? []
            : group.DeviceIds.Split(',').Select(Guid.Parse).ToList();

        foreach (var deviceId in deviceIds)
            currentDeviceIds.Remove(deviceId);

        group.DeviceIds = string.Join(',', currentDeviceIds);
        group.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task<Group?> GetGroupDevicesAsync(
        Guid groupId,
        CancellationToken cancellationToken = default
    ) => await _dbSet.FindAsync([groupId], cancellationToken);
}
