using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.HostedServices;

public class TokenCleanupService(
    IServiceProvider serviceProvider,
    ILogger<TokenCleanupService> logger,
    TokenCleanupStateService stateService
) : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<TokenCleanupService> _logger = logger;
    private readonly TokenCleanupStateService _stateService = stateService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TokenCleanupService started at: {time}", DateTimeOffset.Now);
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(30));
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _logger.LogInformation("TokenCleanupService is working at: {time}", DateTimeOffset.Now);

        using var scope = _serviceProvider.CreateScope();
        var refreshTokenRepository =
            scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();

        _logger.LogInformation("Deleting expired tokens at: {time}", DateTimeOffset.Now);
        refreshTokenRepository.DeleteAllTokens().Wait();

        _stateService.SetLastRun(DateTime.UtcNow);

        _logger.LogInformation("TokenCleanupService completed at: {time}", DateTimeOffset.Now);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TokenCleanupService stopped at: {time}", DateTimeOffset.Now);
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}
