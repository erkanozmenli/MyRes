namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Payment
{
    public sealed record PaymentSucceededIntegrationEvent(Guid TripId, Guid UserId) : IntegrationEvent;
}
