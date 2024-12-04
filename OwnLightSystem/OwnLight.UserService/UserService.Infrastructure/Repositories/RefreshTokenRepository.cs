using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class RefreshTokenRepository(DataContext context) : IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _dbSet = context.Set<RefreshToken>();

    public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
    {
        var createdToken = await _dbSet.AddAsync(refreshToken);
        createdToken.Entity.IsRevoked = false;
        await context.SaveChangesAsync();
        return createdToken.Entity;
    }

    public async Task<RefreshToken?> GetTokenAsync(string refreshToken) =>
        await _dbSet.FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked);

    public async Task<RefreshToken?> GetUserTokenAsync(Guid userId) =>
        await _dbSet
            .Where(rt => rt.UserId == userId)
            .OrderByDescending(rt => rt.CreatedAt)
            .FirstOrDefaultAsync();

    public async Task RevokeTokenAsync(RefreshToken refreshToken)
    {
        refreshToken.ExpiresAt = DateTime.UtcNow;
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.IsRevoked = true;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAllTokens()
    {
        var allTokens = await _dbSet.ToListAsync();
        _dbSet.RemoveRange(allTokens);
        await context.SaveChangesAsync();
    }
}
