using System.Threading.Channels;
using Existential;

namespace LongRunningWorkerService
{
    public sealed class DefaultBackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, ValueTask>> myQueue;

        public DefaultBackgroundTaskQueue(int inCapacity) 
            => myQueue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(
                new BoundedChannelOptions(inCapacity)
                {
                    FullMode = BoundedChannelFullMode.Wait
                });

        public async ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> inWorkItem)
            => await myQueue.Writer.WriteAsync(Validate.ThrowIfNull(inWorkItem, nameof(inWorkItem)));

        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken inCancellationToken)
            => await myQueue.Reader.ReadAsync(inCancellationToken);
    }
}
