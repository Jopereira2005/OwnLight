using DeviceService.Domain.Entities;
using DeviceService.Domain.Enums;

namespace DeviceService.Domain.Interfaces;

public interface IDeviceRepository : IRepository<Device>
{
    Task<Device> ControlDeviceAsync(
        Guid deviceId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<Device> SwitchAsync(
        Guid deviceId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<Device> ControlBrightnessAsync(
        Guid deviceId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<int> ControlUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<int> ControlUserDevicesByGroupIdAsync(
        Guid userId,
        Guid groupId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<int> ControlBrightnessByUserRoomAsync(
        Guid userId,
        Guid roomId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<int> ControlBrightnessByUserGroupAsync(
        Guid userId,
        Guid groupId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<int> ControlAllUserDevicesAsync(
        Guid userId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<Device>> GetDevicesByIdsAsync(Guid[] deviceIds);

    Task<IEnumerable<Device>> GetDevicesByUserIdAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<Device>> GetUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<Device>> GetUserDevicesByGroupIdAsync(
        Guid userId,
        Guid groupId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task DeleteByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default);
}
