namespace LongRunningWorkerService
{
    public class LongRunningWorker(ILogger<LongRunningWorker> inLogger) : BackgroundService
    {
        private const int MillisecondsPerSecond = 1000;

        protected override async Task ExecuteAsync(CancellationToken inStoppingToken)
        {
            while (!inStoppingToken.IsCancellationRequested)
            {
                if (inLogger.IsEnabled(LogLevel.Information))
                {
                    inLogger.LogInformation("LongRunningWorker running at: {time}", DateTimeOffset.Now);
                }

                const int seconds = 1;
                const int duration = seconds * MillisecondsPerSecond;
                await Task.Delay(duration, inStoppingToken);
            }
        }
    }
}
