namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Payment
{
    public sealed record PaymentFailedIntegrationEvent(Guid TripId, Guid UserId, string Message) : IntegrationEvent;
}
