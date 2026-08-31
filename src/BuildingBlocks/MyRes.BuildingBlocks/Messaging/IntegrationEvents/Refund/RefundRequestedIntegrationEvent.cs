namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Refund
{
    public sealed record RefundRequestedIntegrationEvent(Guid TripId) : IntegrationEvent;
}
