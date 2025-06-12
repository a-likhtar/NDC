using Quartz;

namespace NDC.BackgroundJob.Jobs;

public class NasaDataCollectorJob : IJob
{
    private readonly ILogger<NasaDataCollectorJob> _logger;

    public NasaDataCollectorJob(ILogger<NasaDataCollectorJob> logger)
    {
        _logger = logger;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Started data fetching at {Time}", DateTime.UtcNow);
        await FetchDataAsync();
        _logger.LogInformation("Finished data fetching at {Time}", DateTime.UtcNow);
    }

    private async Task FetchDataAsync()
    {
        await Task.Delay(1000);
    }
}