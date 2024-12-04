using AutomationService.Domain.Interfaces;
using AutomationService.Infrastructure.Data;
using AutomationService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationService.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IRoutineRepository, RoutineRepository>();
        services.AddScoped<IRoutineExecutionLogRepository, RoutineExecutionLogRepository>();
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        return services;
    }
}
