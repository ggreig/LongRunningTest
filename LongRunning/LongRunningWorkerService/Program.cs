using App.QueueService;
using LongRunningWorkerService;

var theBuilder = Host.CreateApplicationBuilder(args);
theBuilder.Services.AddSingleton<MonitorLoop>();
theBuilder.Services.AddHostedService<QueuedHostedService>();
theBuilder.Services.AddSingleton<IBackgroundTaskQueue>(_ =>
{
    if (!int.TryParse(theBuilder.Configuration["QueueCapacity"], out var theQueueCapacity))
    {
        theQueueCapacity = 100;
    }

    return new DefaultBackgroundTaskQueue(theQueueCapacity);
});

var theHost = theBuilder.Build();

MonitorLoop theMonitorLoop = theHost.Services.GetRequiredService<MonitorLoop>()!;
theMonitorLoop.StartMonitorLoop();

theHost.Run();
