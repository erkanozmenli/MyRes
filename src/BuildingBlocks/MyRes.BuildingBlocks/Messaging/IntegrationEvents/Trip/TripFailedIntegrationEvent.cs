namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Trip
{
    public sealed record TripFailedIntegrationEvent(Guid TripId, Guid UserId, string Message) : IntegrationEvent;
}
