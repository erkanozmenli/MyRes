namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Booking
{
    public sealed record BookingSucceededIntegrationEvent(Guid TripId, Guid UserId) : IntegrationEvent;
}
