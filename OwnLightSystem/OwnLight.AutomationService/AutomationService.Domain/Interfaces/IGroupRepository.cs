using AutomationService.Domain.Entities;

namespace AutomationService.Domain.Interfaces;

public interface IGroupRepository : IRepository<Group>
{
    public Task<IEnumerable<Group>> GetUserGroupsAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    public Task<Group?> GetUserGroupByNameAsync(
        Guid userId,
        string groupName,
        CancellationToken cancellationToken = default
    );

    public Task AddDevicesToGroupAsync(
        Guid groupId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    );

    public Task RemoveDevicesFromGroupAsync(
        Guid groupId,
        Guid[] deviceIds,
        CancellationToken cancellationToken = default
    );

    public Task<Group?> GetGroupDevicesAsync(
        Guid groupId,
        CancellationToken cancellationToken = default
    );
}
