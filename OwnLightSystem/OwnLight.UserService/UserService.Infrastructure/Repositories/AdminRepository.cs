using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class AdminRepository(DataContext context) : IAdminRepository
{
    private readonly DbSet<User> _dbSet = context.Set<User>();

    public async Task<User?> DeleteAllAsync()
    {
        var users = await _dbSet.Where(u => u.Username != "admin").ToListAsync();
        _dbSet.RemoveRange(users);
        await context.SaveChangesAsync();
        return null;
    }
}
