namespace AutomationService.Domain.Interfaces;

public interface IRepository<T>
    where T : class
{
    public Task<IEnumerable<T>> GetAllAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );
    public Task<T?> GetByIdAsync(Guid id);
    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<int> CountAsync(CancellationToken cancellationToken = default);
}
