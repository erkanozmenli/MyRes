using MassTransit;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Trip;
using MyRes.NotificationService.Application.Abstractions;
using MyRes.NotificationService.Domain.Notifications;

namespace MyRes.NotificationService.Application.Notifications.TripCompleted
{
    public class TripCompletedIntegrationEventHandler
        (INotificationPublisher publisher) : IConsumer<TripCompletedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<TripCompletedIntegrationEvent> context)
        {
            var message = NotificationMessage.TripCompleted(context.Message.TripId, context.Message.UserId);

            await publisher.PublishAsync(message, context.CancellationToken);
        }
    }
}
