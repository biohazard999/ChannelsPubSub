
using ChannelsPubSub.Protocol;

namespace ChannelsPubSub.Services;

public sealed class ProducingTimedHostedService(IPubSub<BackgroundTimeMessage> publisher) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while(await timer.WaitForNextTickAsync(stoppingToken))
        {
            await publisher.Writer.WriteAsync(new BackgroundTimeMessage(DateTime.UtcNow), stoppingToken);
        }
    }
}
