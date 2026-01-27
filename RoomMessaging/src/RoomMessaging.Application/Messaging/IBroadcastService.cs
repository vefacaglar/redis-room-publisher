namespace RoomMessaging.Application.Messaging
{
    public interface IBroadcastService
    {
        Task BroadcastAsync(string channel, string message, CancellationToken cancellationToken = default);
    }
}