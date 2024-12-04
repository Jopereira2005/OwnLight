using DeviceService.Domain.Interfaces;
using DeviceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Infrastructure.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    protected readonly DataContext _dataContext;
    private readonly DbSet<T> _dbSet;

    public Repository(DataContext dataContext)
    {
        _dataContext = dataContext;
        _dbSet = _dataContext.Set<T>();
    }

    protected async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await _dataContext.SaveChangesAsync(cancellationToken);

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T?> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return null;
        _dbSet.Remove(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        var skipAmount = (page - 1) * pageSize;
        return await _dbSet.Skip(skipAmount).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<int> CountAsync() => await _dbSet.CountAsync();
}
