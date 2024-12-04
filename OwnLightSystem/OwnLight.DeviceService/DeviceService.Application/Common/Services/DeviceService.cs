using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Interfaces;

namespace DeviceService.Application.Common.Services;

public class DeviceService(IDeviceRepository deviceRepository) : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;

    public async Task<Device> GetDeviceByIdAsync(
        Guid deviceId,
        CancellationToken cancellationToken = default
    )
    {
        var device =
            await _deviceRepository.GetByIdAsync(deviceId)
            ?? throw new KeyNotFoundException($"Dispositivo com ID {deviceId} não encontrado.");

        return device;
    }

    public async Task<IEnumerable<Device>> GetUserDevicesAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var devices =
            await _deviceRepository.GetDevicesByUserIdAsync(
                userId,
                pageNumber,
                pageSize,
                cancellationToken
            )
            ?? throw new KeyNotFoundException(
                $"Dispositivos do usuário com ID {userId} não encontrados."
            );

        return devices;
    }

    public async Task<IEnumerable<Device>> GetUserDevicesByGroupIdAsync(
        Guid userId,
        Guid groupId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var devices =
            await _deviceRepository.GetUserDevicesByGroupIdAsync(
                userId,
                groupId,
                pageNumber,
                pageSize,
                cancellationToken
            )
            ?? throw new KeyNotFoundException(
                $"Dispositivos do grupo com ID {groupId} não encontrados."
            );

        return devices;
    }

    public async Task<IEnumerable<Device>> GetUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var devices =
            await _deviceRepository.GetUserDevicesByRoomIdAsync(
                userId,
                roomId,
                pageNumber,
                pageSize,
                cancellationToken
            )
            ?? throw new KeyNotFoundException(
                $"Dispositivos do cômodo com ID {roomId} não encontrados."
            );

        return devices;
    }

    public async Task<IEnumerable<Device>> GetDevicesByUserIdAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var devices =
            await _deviceRepository.GetDevicesByUserIdAsync(
                userId,
                pageNumber,
                pageSize,
                cancellationToken
            )
            ?? throw new KeyNotFoundException(
                $"Dispositivos do usuário com ID {userId} não encontrados."
            );

        return devices;
    }

    public async Task ControlDeviceAsync(
        Guid deviceId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    ) => await _deviceRepository.ControlDeviceAsync(deviceId, status, cancellationToken);

    public async Task ControlAllUserDevicesAsync(
        Guid userId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    ) => await _deviceRepository.ControlAllUserDevicesAsync(userId, status, cancellationToken);

    public async Task ControlDeviceBrightnessAsync(
        Guid deviceId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        await _deviceRepository.ControlBrightnessAsync(
            deviceId,
            brightness,
            status,
            cancellationToken
        );
    }

    public async Task ControlBrightnessByUserGroupAsync(
        Guid userId,
        Guid groupId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        await _deviceRepository.ControlBrightnessByUserGroupAsync(
            userId,
            groupId,
            brightness,
            status,
            cancellationToken
        );
    }

    public async Task ControlBrightnessByUserRoomAsync(
        Guid userId,
        Guid roomId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        await _deviceRepository.ControlBrightnessByUserRoomAsync(
            userId,
            roomId,
            brightness,
            status,
            cancellationToken
        );
    }

    public async Task ControlUserDevicesByGroupIdAsync(
        Guid userId,
        Guid groupId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        await _deviceRepository.ControlUserDevicesByGroupIdAsync(
            userId,
            groupId,
            status,
            cancellationToken
        );
    }

    public async Task ControlUserDevicesByRoomIdAsync(
        Guid userId,
        Guid roomId,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        await _deviceRepository.ControlUserDevicesByRoomIdAsync(
            userId,
            roomId,
            status,
            cancellationToken
        );
    }

    public async Task SwitchDeviceAsync(
        Guid deviceId,
        int brightness,
        DeviceStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var device =
            await GetDeviceByIdAsync(deviceId, cancellationToken)
            ?? throw new KeyNotFoundException($"Dispositivo com ID {deviceId} não encontrado.");

        if (device.Status == DeviceStatus.On)
        {
            device = await _deviceRepository.SwitchAsync(
                deviceId,
                DeviceStatus.Off,
                cancellationToken
            );
            device.Brightness = 0;
        }
        else
        {
            device = await _deviceRepository.SwitchAsync(
                deviceId,
                DeviceStatus.On,
                cancellationToken
            );
            device.Brightness = brightness;
        }
    }
}
