using NDC.BackgroundJob.Jobs;
using Quartz;

namespace NDC.BackgroundJob;

public class DataCollectorBackgroundService : BackgroundService
{
    private readonly ILogger<DataCollectorBackgroundService> _logger;
    private readonly ISchedulerFactory _schedulerFactory;
    private IScheduler _scheduler;

    public DataCollectorBackgroundService(ILogger<DataCollectorBackgroundService> logger, ISchedulerFactory schedulerFactory)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Background Job Started");

        _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        await _scheduler.Start(cancellationToken);

        await RegisterJobsAsync();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _scheduler.Shutdown(cancellationToken);
    }

    private async Task RegisterJobsAsync()
    {
        var jobKey = new JobKey("NasaDataCollectorJob");

        if (!await _scheduler.CheckExists(jobKey))
        {
            var job = JobBuilder.Create<NasaDataCollectorJob>()
                .WithIdentity(jobKey)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("NasaDataCollectorTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3600)
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }
    }
}