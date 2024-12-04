using AutomationService.Domain.Entities;

namespace AutomationService.Domain.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    public Task<IEnumerable<Room>> GetUserRoomsAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    public Task<Room?> GetUserRoomByNameAsync(
        Guid userId,
        string roomName,
        CancellationToken cancellationToken = default
    );

    public Task AddDevicesToRoomAsync(
        Guid roomId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    );

    public Task RemoveDevicesFromRoomAsync(
        Guid roomId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    );

    public Task<Room?> GetRoomDevicesAsync(
        Guid roomId,
        CancellationToken cancellationToken = default
    );
}
