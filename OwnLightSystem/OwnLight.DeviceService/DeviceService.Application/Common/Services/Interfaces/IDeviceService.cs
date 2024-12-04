using DeviceService.Domain.Entities;
using DeviceService.Domain.Enums;

namespace DeviceService.Application.Common.Services.Interfaces;

public interface IDeviceService
{
    Task<Device> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Device>> GetUserDevicesAsync(
        Guid userId,
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

    Task<IEnumerable<Device>> GetUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task ControlDeviceAsync(
        Guid deviceId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task ControlAllUserDevicesAsync(
        Guid userId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task SwitchDeviceAsync(
        Guid deviceId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task ControlDeviceBrightnessAsync(
        Guid deviceId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task ControlBrightnessByUserGroupAsync(
        Guid userId,
        Guid groupId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task ControlBrightnessByUserRoomAsync(
        Guid userId,
        Guid roomId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task ControlUserDevicesByGroupIdAsync(
        Guid userId,
        Guid groupId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );

    Task ControlUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    );
}
