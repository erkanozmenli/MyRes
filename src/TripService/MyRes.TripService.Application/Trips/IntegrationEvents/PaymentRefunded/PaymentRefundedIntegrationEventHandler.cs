using MassTransit;
using Microsoft.Extensions.Logging;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Refund;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;

namespace MyRes.TripService.Application.Trips.IntegrationEvents.PaymentRefunded
{
    public class PaymentRefundedIntegrationEventHandler
        (ITripRepository tripRepository, ILogger<PaymentRefundedIntegrationEventHandler> logger) : IConsumer<PaymentRefundedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<PaymentRefundedIntegrationEvent> context)
        {
            logger.LogInformation("Consumed {EventName} for Trip {TripId}", nameof(PaymentRefundedIntegrationEvent), context.Message.TripId);

            var trip = await tripRepository.GetByGuidIdAsync(context.Message.TripId);

            if (trip is null)
                throw new TripNotFoundException(context.Message.TripId);

            trip.CompleteRefund();

            await tripRepository.SaveChangesAsync();
        }
    }
}
