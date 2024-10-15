using ChannelsPubSub.Services;

namespace ChannelsPubSub.Protocol;

public sealed record BackgroundTimeMessage(DateTime DateTime) : PubSubMessage;

public sealed record ClickTimeMessage(DateTime DateTime) : PubSubMessage;