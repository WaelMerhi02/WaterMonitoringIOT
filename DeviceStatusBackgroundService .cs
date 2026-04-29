namespace WaterMonitoringIOT
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class DeviceStatusBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DeviceStatusBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<WaterMonitoringIOTDbContext>();

                    await context.Database.ExecuteSqlRawAsync(@"
                    UPDATE Devices
                    SET IsActive = 0
                    WHERE IsActive = 1
                    AND LastSeenAt IS NOT NULL
                    AND DATEDIFF(
                    SECOND,
                    LastSeenAt,
                    CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Middle East Standard Time' AS datetime)
                    ) > 60
                ", stoppingToken);
                }
                catch
                {
                    // silently ignore errors (optional: you can remove try-catch if you prefer crashing on error)
                }

                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }
    }
}
