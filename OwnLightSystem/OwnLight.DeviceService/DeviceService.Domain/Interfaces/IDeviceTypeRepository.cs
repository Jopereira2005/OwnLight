using DeviceService.Domain.Entities;

namespace DeviceService.Domain.Interfaces;

public interface IDeviceTypeRepository : IRepository<DeviceType>
{
    Task<DeviceType?> GetDeviceTypeByNameAsync(
        string typeName,
        CancellationToken cancellationToken = default
    );
}
