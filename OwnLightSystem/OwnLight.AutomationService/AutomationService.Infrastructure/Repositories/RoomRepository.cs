using AutomationService.Domain.Entities;
using AutomationService.Domain.Interfaces;
using AutomationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomationService.Infrastructure.Repositories;

public class RoomRepository(DataContext dataContext)
    : Repository<Room>(dataContext),
        IRoomRepository
{
    private readonly DbSet<Room> _dbSet = dataContext.Set<Room>();

    public async Task<IEnumerable<Room>> GetUserRoomsAsync(
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

    public async Task<Room?> GetUserRoomByNameAsync(
        Guid userId,
        string roomName,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet
            .Where(r => r.UserId == userId && r.Name == roomName)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddDevicesToRoomAsync(
        Guid roomId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    )
    {
        var room = await _dbSet.FindAsync([roomId], cancellationToken);
        var currentDeviceIds = string.IsNullOrEmpty(room!.DeviceIds)
            ? []
            : room.DeviceIds.Split(',').Select(Guid.Parse).ToList();

        foreach (var deviceId in deviceIds)
            if (!currentDeviceIds.Contains(deviceId))
                currentDeviceIds.Add(deviceId);

        room.DeviceIds = string.Join(',', currentDeviceIds);
        room.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveDevicesFromRoomAsync(
        Guid roomId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    )
    {
        var room = await _dbSet.FindAsync([roomId], cancellationToken);
        var currentDeviceIds = string.IsNullOrEmpty(room!.DeviceIds)
            ? []
            : room.DeviceIds.Split(',').Select(Guid.Parse).ToList();

        foreach (var deviceId in deviceIds)
            currentDeviceIds.Remove(deviceId);

        room.DeviceIds = string.Join(',', currentDeviceIds);
        room.UpdatedAt = DateTime.UtcNow;

        await SaveChangesAsync(cancellationToken);
    }

    public async Task<Room?> GetRoomDevicesAsync(
        Guid roomId,
        CancellationToken cancellationToken = default
    ) => await _dbSet.FindAsync([roomId], cancellationToken);
}
