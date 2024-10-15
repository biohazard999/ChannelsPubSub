
using ChannelsPubSub.Protocol;

namespace ChannelsPubSub.Services;

public sealed class ConsumingTimedHostedService(
    IPubSub<ClickTimeMessage> subscriber,
    ILogger<ConsumingTimedHostedService> logger
) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(await subscriber.Reader.WaitToReadAsync(stoppingToken))
        {
            var message = await subscriber.Reader.ReadAsync(stoppingToken);
            logger.LogInformation("Received click time message: {DateTime:F}", message.DateTime.ToLocalTime());
        }
    }
}
