namespace LongRunningWorkerService
{
    public class LongRunningWorker : BackgroundService
    {
        private readonly ILogger<LongRunningWorker> _logger;

        public LongRunningWorker(ILogger<LongRunningWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("LongRunningWorker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
