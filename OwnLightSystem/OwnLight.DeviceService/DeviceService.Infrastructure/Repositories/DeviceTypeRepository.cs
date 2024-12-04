using DeviceService.Domain.Entities;
using DeviceService.Domain.Interfaces;
using DeviceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Infrastructure.Repositories;

public class DeviceTypeRepository(DataContext dataContext)
    : Repository<DeviceType>(dataContext),
        IDeviceTypeRepository
{
    private readonly DbSet<DeviceType> _dbSet = dataContext.Set<DeviceType>();

    public async Task<DeviceType?> GetDeviceTypeByNameAsync(
        string typeName,
        CancellationToken cancellationToken = default
    ) => await _dbSet.FirstOrDefaultAsync(dt => dt.TypeName == typeName, cancellationToken);
}
