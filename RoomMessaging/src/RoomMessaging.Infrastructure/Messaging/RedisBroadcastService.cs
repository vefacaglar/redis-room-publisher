using RoomMessaging.Application.Messaging;
using StackExchange.Redis;

namespace RoomMessaging.Infrastructure.Messaging
{
    public class RedisBroadcastService : IBroadcastService
    {
        private readonly IConnectionMultiplexer _mux;
        private readonly string _channelPrefix;

        public RedisBroadcastService(
                IConnectionMultiplexer mux,
                string channelPrefix = "channel"
            )
        {
            _mux = mux;
            _channelPrefix = string.IsNullOrWhiteSpace(channelPrefix) ? "channel" : channelPrefix;
        }

        public async Task BroadcastAsync(string channel, string message, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentException("Channel is required.", nameof(channel));

            if (message is null)
                throw new ArgumentNullException(nameof(message));

            var fullChannel = RedisChannel.Literal($"{_channelPrefix}:{channel}");

            var sub = _mux.GetSubscriber();
            await sub.PublishAsync(fullChannel, message);
        }
    }
}