namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Payment
{
    public sealed record PaymentRequestedIntegrationEvent(Guid TripId, Guid UserId) : IntegrationEvent;
}
