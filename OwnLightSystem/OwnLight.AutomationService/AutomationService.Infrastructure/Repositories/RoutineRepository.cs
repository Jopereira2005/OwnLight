using AutomationService.Domain.Entities;
using AutomationService.Domain.Interfaces;
using AutomationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomationService.Infrastructure.Repositories;

public class RoutineRepository(DataContext dataContext)
    : Repository<Routine>(dataContext),
        IRoutineRepository
{
    private readonly DbSet<Routine> _dbSet = dataContext.Set<Routine>();

    public async Task<IEnumerable<Routine>> GetUserRoutinesAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var skipAmount = (page - 1) * pageSize;
        return await _dbSet
            .Where(r => r.UserId == userId)
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Routine?> GetRoutineByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    ) => await _dbSet.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
}
