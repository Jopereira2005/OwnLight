namespace UserService.Infrastructure.HostedServices;

public class TokenCleanupStateService
{
    private DateTime? _lastRunTime;

    public DateTime? GetLastRun() => _lastRunTime;

    public void SetLastRun(DateTime time) => _lastRunTime = time;
}
