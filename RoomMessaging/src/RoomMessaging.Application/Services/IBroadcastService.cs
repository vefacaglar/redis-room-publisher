namespace RoomMessaging.Application.Services
{
    public interface IBroadcastService
    {
        Task BroadcastAsync<T>(string channel, T message);
    }
}