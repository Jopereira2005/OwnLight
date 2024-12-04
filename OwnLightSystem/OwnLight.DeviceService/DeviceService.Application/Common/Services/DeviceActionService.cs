using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;

namespace DeviceService.Application.Common.Services;

public class DeviceActionService(IDeviceActionRepository deviceActionRepository)
    : IDeviceActionService
{
    private readonly IDeviceActionRepository _deviceActionRepository = deviceActionRepository;

    public async Task LogDeviceActionAsync(
        DeviceAction deviceAction,
        CancellationToken cancellationToken = default
    ) => await _deviceActionRepository.CreateAsync(deviceAction, cancellationToken);

    public async Task LogDeviceActionsAsync(
        IEnumerable<DeviceAction> deviceActions,
        CancellationToken cancellationToken = default
    ) => await _deviceActionRepository.AddDeviceActionsAsync(deviceActions, cancellationToken);
}
