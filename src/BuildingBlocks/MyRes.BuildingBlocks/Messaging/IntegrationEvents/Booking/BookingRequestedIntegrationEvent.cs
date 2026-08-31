namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Booking
{
    public sealed record BookingRequestedIntegrationEvent(Guid TripId, Guid UserId) : IntegrationEvent;
}
