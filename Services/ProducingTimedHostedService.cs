
using ChannelsPubSub.Protocol;

using Microsoft.Extensions.Logging;

namespace ChannelsPubSub.Services;

public sealed class ProducingTimedHostedService(
    IPubSub<BackgroundTimeMessage> publisher,
    ILogger<ProducingTimedHostedService> logger
) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while(await timer.WaitForNextTickAsync(stoppingToken))
        {
            var message = new BackgroundTimeMessage(DateTime.UtcNow);   
            logger.LogInformation("Publishing time message: {DateTime:F}", message.DateTime.ToLocalTime());
            await publisher.Writer.WriteAsync(message, stoppingToken);
        }
    }
}
