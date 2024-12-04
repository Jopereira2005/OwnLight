using AutomationService.Domain.Interfaces;
using AutomationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomationService.Infrastructure.Repositories;

public class Repository<T>(DataContext dataContext) : IRepository<T>
    where T : class
{
    protected readonly DataContext _dataContext = dataContext;
    private readonly DbSet<T> _dbSet = dataContext.Set<T>();

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return null;
        _dbSet.Remove(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (pageNumber - 1) * pageSize;
        return await _dbSet.Skip(skipAmount).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    protected async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await _dataContext.SaveChangesAsync(cancellationToken);

    public async Task<int> CountAsync(CancellationToken cancellationToken) =>
        await _dbSet.CountAsync(cancellationToken);
}
