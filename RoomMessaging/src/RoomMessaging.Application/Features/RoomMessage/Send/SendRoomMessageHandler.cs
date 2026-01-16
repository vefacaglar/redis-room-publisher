using MediatR;
using Microsoft.Extensions.Logging;

namespace RoomMessaging.Application.Features.RoomMessage.Send
{
    public class SendRoomMessageNotification : INotification
    {
        public string Name { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }

    public class SendRoomMessageNotificationHandler : INotificationHandler<SendRoomMessageNotification>
    {
        private readonly ILogger<SendRoomMessageNotificationHandler> _logger;

        public SendRoomMessageNotificationHandler(
            ILogger<SendRoomMessageNotificationHandler> logger
        )
        {
            _logger = logger;
        }

        public async Task Handle(SendRoomMessageNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending message to room {RoomName}: {Message}", notification.Name, notification.Message);
        }
    }
}