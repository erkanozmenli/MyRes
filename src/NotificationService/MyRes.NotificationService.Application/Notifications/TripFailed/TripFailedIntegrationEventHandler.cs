using MassTransit;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Trip;
using MyRes.NotificationService.Application.Abstractions;
using MyRes.NotificationService.Domain.Notifications;

namespace MyRes.NotificationService.Application.Notifications.TripFailed
{
    public class TripFailedIntegrationEventHandler
        (INotificationPublisher publisher) : IConsumer<TripFailedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<TripFailedIntegrationEvent> context)
        {
            var message = NotificationMessage.TripFailed(context.Message.TripId, context.Message.UserId, context.Message.Message);
            await publisher.PublishAsync(message, context.CancellationToken);
        }
    }
}
