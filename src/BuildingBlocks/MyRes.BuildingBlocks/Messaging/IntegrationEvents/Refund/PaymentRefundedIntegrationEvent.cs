namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Refund
{
    public sealed record PaymentRefundedIntegrationEvent(Guid TripId) : IntegrationEvent;
}
