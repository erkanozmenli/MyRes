using MyRes.NotificationService.Domain.Notifications;

namespace MyRes.NotificationService.Application.Abstractions
{
    public interface INotificationPublisher
    {
        Task PublishAsync(NotificationMessage message, CancellationToken cancellationToken);
    }
}
