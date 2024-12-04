using DeviceService.Domain.Entities;

namespace DeviceService.Application.Common.Services.Interfaces;

public interface IDeviceActionService
{
    Task LogDeviceActionAsync(
        DeviceAction deviceAction,
        CancellationToken cancellationToken = default
    );

    Task LogDeviceActionsAsync(
        IEnumerable<DeviceAction> deviceActions,
        CancellationToken cancellationToken = default
    );
}
