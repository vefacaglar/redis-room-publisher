using Microsoft.Extensions.DependencyInjection;
using RoomMessaging.Application.Messaging;
using RoomMessaging.Infrastructure.Messaging;
using StackExchange.Redis;

namespace RoomMessaging.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var redisConnStr = config["Redis:ConnectionString"];
            if (string.IsNullOrWhiteSpace(redisConnStr))
                throw new InvalidOperationException("Missing config: Redis:ConnectionString");

            var channelPrefix = config["Redis:ChannelPrefix"] ?? "room";

            // ConnectionMultiplexer thread-safe -> singleton
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(redisConnStr));

            services.AddSingleton<IBroadcastService>(sp =>
                new RedisBroadcastService(sp.GetRequiredService<IConnectionMultiplexer>(), channelPrefix));

            return services;
        }
    }
}