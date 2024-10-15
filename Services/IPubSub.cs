using System.Threading.Channels;

namespace ChannelsPubSub.Services;

public interface IPubSub<TMessage>
    where TMessage : PubSubMessage
{
    Channel<TMessage> Channel { get; }

    ChannelReader<TMessage> Reader { get; }

    ChannelWriter<TMessage> Writer { get; }
}

public sealed class PubSub<TMessage> : IPubSub<TMessage>
    where TMessage : PubSubMessage
{
    public Channel<TMessage> Channel { get; } = System.Threading.Channels.Channel.CreateUnbounded<TMessage>();    

    public ChannelReader<TMessage> Reader => Channel.Reader;
    public ChannelWriter<TMessage> Writer => Channel.Writer;
}

public abstract record PubSubMessage;

