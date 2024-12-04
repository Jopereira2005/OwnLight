using DeviceService.Domain.Entities;
using DeviceService.Domain.Enums;

namespace DeviceService.Domain.Interfaces;

public interface IDeviceActionRepository : IRepository<DeviceAction>
{
    Task AddDeviceActionsAsync(
        IEnumerable<DeviceAction> deviceActions,
        CancellationToken cancellationToken = default
    );
    
    Task<IEnumerable<DeviceAction>> GetUserActionsAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<DeviceAction>> GetActionsByDeviceIdAsync(
        Guid deviceId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );
    Task<IEnumerable<DeviceAction>> GetUserActionsByStatusAsync(
        Guid userId,
        ActionStatus actionStatus,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<DeviceAction>> GetUserActionsByTypeAsync(
        Guid userId,
        DeviceActions actionType,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<DeviceAction>> GetActionsByTypeAsync(
        DeviceActions actionType,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<DeviceAction>> GetActionsByStatusAsync(
        ActionStatus actionStatus,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );
}
