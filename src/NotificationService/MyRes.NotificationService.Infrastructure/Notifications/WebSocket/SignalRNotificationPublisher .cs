using Microsoft.AspNetCore.SignalR;
using MyRes.NotificationService.Application.Abstractions;
using MyRes.NotificationService.Domain.Notifications;

namespace MyRes.NotificationService.Infrastructure.Notifications.WebSocket
{
    public class SignalRNotificationPublisher(IHubContext<NotificationHub> hubContext) : INotificationPublisher
    {
        public async Task PublishAsync(NotificationMessage message, CancellationToken cancellationToken)
        {
            await hubContext.Clients.Group(message.UserId.ToString()).SendAsync("checkoutNotification", message, cancellationToken);
        }
    }
}
