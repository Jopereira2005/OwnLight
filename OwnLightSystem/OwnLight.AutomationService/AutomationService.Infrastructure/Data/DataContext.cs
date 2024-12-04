using AutomationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationService.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Routine> Routines { get; set; }
    public DbSet<RoutineExecutionLog> RoutineExecutionLogs { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Routine>().HasKey(r => r.Id);
        modelBuilder
            .Entity<Routine>()
            .HasMany(r => r.ExecutionLogs)
            .WithOne(log => log.Routine)
            .HasForeignKey(log => log.RoutineId);

        modelBuilder.Entity<RoutineExecutionLog>().HasKey(log => log.Id);

        modelBuilder.Entity<Group>().HasKey(g => g.Id);
        modelBuilder.Entity<Group>().Property(g => g.DeviceIds).HasColumnName("DeviceIds");

        modelBuilder.Entity<Room>().HasKey(r => r.Id);
        modelBuilder.Entity<Room>().Property(r => r.DeviceIds).HasColumnName("DeviceIds");

        base.OnModelCreating(modelBuilder);
    }
}
