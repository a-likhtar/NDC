using NDC.Domain.Services;
using Quartz;

namespace NDC.BackgroundJob.Jobs;

public class NasaDataCollectorJob : IJob
{
    private readonly ILogger<NasaDataCollectorJob> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public NasaDataCollectorJob(ILogger<NasaDataCollectorJob> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Started data fetching at {Time}", DateTime.UtcNow);
        using var scope = _scopeFactory.CreateScope();
        
        var itemSyncService = scope.ServiceProvider.GetRequiredService<IItemSyncService>();
        await itemSyncService.SyncItemsAsync();
        _logger.LogInformation("Finished data fetching at {Time}", DateTime.UtcNow);
    }
}