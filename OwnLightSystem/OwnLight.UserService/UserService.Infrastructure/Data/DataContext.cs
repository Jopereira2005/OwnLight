using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<RefreshToken> Tokens { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração do relacionamento entre User e RefreshToken
        modelBuilder
            .Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(rt => rt.UserId);

        base.OnModelCreating(modelBuilder);
    }
}
