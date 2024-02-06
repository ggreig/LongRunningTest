using LongRunningWorkerService;

namespace App.QueueService;

public sealed class MonitorLoop(IBackgroundTaskQueue myTaskQueue, ILogger<MonitorLoop> myLogger, IHostApplicationLifetime myApplicationLifetime)
{
    private readonly CancellationToken myCancellationToken = myApplicationLifetime.ApplicationStopping;

    public void StartMonitorLoop()
    {
        myLogger.LogInformation($"{nameof(MonitorAsync)} loop is starting.");

        // Run a console user input loop in a background thread
        Task.Run(async () => await MonitorAsync());
    }

    private async ValueTask MonitorAsync()
    {
        while (!myCancellationToken.IsCancellationRequested)
        {
            var theKeyStroke = Console.ReadKey();
            if (theKeyStroke.Key == ConsoleKey.W)
            {
                // Enqueue a background work item
                await myTaskQueue.QueueBackgroundWorkItemAsync(BuildWorkItemAsync);
            }
        }
    }

    private async ValueTask BuildWorkItemAsync(CancellationToken inToken)
    {
        // Simulate three 5-second tasks to complete
        // for each enqueued work item

        int theDelayLoop = 0;
        var theGuid = Guid.NewGuid();

        myLogger.LogInformation("Queued work item {Guid} is starting.", theGuid);

        while (!inToken.IsCancellationRequested && theDelayLoop < 3)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5), inToken);
            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if the Delay is cancelled
            }

            ++theDelayLoop;

            myLogger.LogInformation("Queued work item {Guid} is running. {DelayLoop}/3", theGuid, theDelayLoop);
        }

        if (theDelayLoop is 3)
        {
            myLogger.LogInformation("Queued Background Task {Guid} is complete.", theGuid);
        }
        else
        {
            myLogger.LogInformation("Queued Background Task {Guid} was cancelled.", theGuid);
        }
    }
}