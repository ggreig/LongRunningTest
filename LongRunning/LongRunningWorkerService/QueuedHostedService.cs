using LongRunningWorkerService;

namespace App.QueueService;

public sealed class QueuedHostedService(IBackgroundTaskQueue myTaskQueue, ILogger<QueuedHostedService> myLogger) 
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        myLogger.LogInformation("""
                              {Name} is running.
                              Tap W to add a work item to the
                              background queue.
                              """,
            nameof(QueuedHostedService));

        return ProcessTaskQueueAsync(stoppingToken);
    }

    private async Task ProcessTaskQueueAsync(CancellationToken inCancellationToken)
    {
        while (!inCancellationToken.IsCancellationRequested)
        {
            try
            {
                Func<CancellationToken, ValueTask>? theWorkItem =
                    await myTaskQueue.DequeueAsync(inCancellationToken);

                await theWorkItem(inCancellationToken);
            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if inCancellationToken was signaled
            }
            catch (Exception theException)
            {
                myLogger.LogError(theException, "Error occurred executing task work item.");
            }
        }
    }

    public override async Task StopAsync(CancellationToken inCancellationToken)
    {
        myLogger.LogInformation(
            $"{nameof(QueuedHostedService)} is stopping.");

        await base.StopAsync(inCancellationToken);
    }
}