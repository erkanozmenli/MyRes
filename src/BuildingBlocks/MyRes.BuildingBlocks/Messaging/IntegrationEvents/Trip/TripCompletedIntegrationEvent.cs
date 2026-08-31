namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Trip
{
    public sealed record TripCompletedIntegrationEvent(Guid TripId, Guid UserId) : IntegrationEvent;
}
