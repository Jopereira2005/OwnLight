using DeviceService.Domain.Entities;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;
using DeviceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Infrastructure.Repositories;

public class DeviceRepository(DataContext dataContext)
    : Repository<Device>(dataContext),
        IDeviceRepository
{
    private readonly DbSet<Device> _dbSet = dataContext.Set<Device>();

    public async Task<Device> ControlDeviceAsync(
        Guid deviceId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var device =
            await _dbSet.FindAsync(deviceId)
            ?? throw new KeyNotFoundException("Dispositivo não encontrado");

        device.Status = status;
        await SaveChangesAsync(cancellationToken);
        return device;
    }

    public async Task<Device> SwitchAsync(
        Guid deviceId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var device =
            await _dbSet.FindAsync(deviceId)
            ?? throw new KeyNotFoundException("Dispositivo não encontrado");

        device.Status = device.Status == DeviceStatus.On ? DeviceStatus.Off : DeviceStatus.On;
        await SaveChangesAsync(cancellationToken);
        return device;
    }

    public async Task<Device> ControlBrightnessAsync(
        Guid deviceId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var device =
            await _dbSet.FindAsync(deviceId)
            ?? throw new KeyNotFoundException("Dispositivo não encontrado");

        device.Brightness = brightness;
        device.Status = status;
        await SaveChangesAsync(cancellationToken);
        return device;
    }

    public async Task<int> ControlUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var rowsAffected = await _dbSet
            .Where(d => d.UserId == userId && d.RoomId == roomId)
            .ExecuteUpdateAsync(d => d.SetProperty(p => p.Status, status), cancellationToken);

        if (rowsAffected == 0)
            throw new KeyNotFoundException(
                $"Nenhum dispositivo encontrado para o usuário {userId} no cômodo {roomId}"
            );

        return rowsAffected;
    }

    public async Task<int> ControlUserDevicesByGroupIdAsync(
        Guid userId,
        Guid groupId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var rowsAffected = await _dbSet
            .Where(d => d.UserId == userId && d.GroupId == groupId)
            .ExecuteUpdateAsync(d => d.SetProperty(p => p.Status, status), cancellationToken);

        if (rowsAffected == 0)
            throw new KeyNotFoundException(
                $"Nenhum dispositivo encontrado para o usuário {userId} no grupo {groupId}"
            );

        return rowsAffected;
    }

    public async Task<int> ControlBrightnessByUserRoomAsync(
        Guid userId,
        Guid roomId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var rowsAffected = await _dbSet
            .Where(d => d.UserId == userId && d.RoomId == roomId && d.IsDimmable == true)
            .ExecuteUpdateAsync(
                d =>
                    d.SetProperty(p => p.Brightness, brightness).SetProperty(p => p.Status, status),
                cancellationToken
            );

        if (rowsAffected == 0)
            throw new KeyNotFoundException(
                $"Nenhum dispositivo dimerizável encontrado para o usuário {userId} no cômodo {roomId}"
            );

        return rowsAffected;
    }

    public async Task<int> ControlBrightnessByUserGroupAsync(
        Guid userId,
        Guid groupId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var rowsAffected = await _dbSet
            .Where(d => d.UserId == userId && d.GroupId == groupId && d.IsDimmable == true)
            .ExecuteUpdateAsync(
                d =>
                    d.SetProperty(p => p.Brightness, brightness).SetProperty(p => p.Status, status),
                cancellationToken
            );

        if (rowsAffected == 0)
            throw new KeyNotFoundException(
                $"Nenhum dispositivo dimerizável encontrado para o usuário {userId} no grupo {groupId}"
            );

        return rowsAffected;
    }

    public async Task<int> ControlAllUserDevicesAsync(
        Guid userId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var rowsAffected = await _dbSet
            .Where(d => d.UserId == userId)
            .ExecuteUpdateAsync(d => d.SetProperty(p => p.Status, status), cancellationToken);

        if (rowsAffected == 0)
            throw new KeyNotFoundException(
                $"Nenhum dispositivo encontrado para o usuário {userId}"
            );

        return rowsAffected;
    }

    public async Task<IEnumerable<Device>> GetDevicesByIdsAsync(Guid[] deviceIds)
    {
        return await _dbSet
            .Include(d => d.DeviceType)
            .Where(d => deviceIds.Contains(d.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<Device>> GetDevicesByUserIdAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Include(d => d.DeviceType)
            .Where(d => d.UserId == userId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Device>> GetUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Include(d => d.DeviceType)
            .Where(d => d.UserId == userId && d.RoomId == roomId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Device>> GetUserDevicesByGroupIdAsync(
        Guid userId,
        Guid groupId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet
            .Include(d => d.DeviceType)
            .Where(d => d.UserId == userId && d.GroupId == groupId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
