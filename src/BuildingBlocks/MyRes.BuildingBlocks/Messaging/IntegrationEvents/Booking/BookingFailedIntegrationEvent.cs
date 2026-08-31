namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents.Booking
{
    public sealed record BookingFailedIntegrationEvent(Guid TripId, Guid UserId, string Message) : IntegrationEvent;
}
