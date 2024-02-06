namespace LongRunningWorkerService
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> inWorkItem);
        
        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken inCancellationToken);
    }
}
