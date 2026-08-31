namespace MyRes.TripService.Application.Trips.Commands.AddFlightReservation.DTOs
{
    public sealed record FlightSegmentDto(
       string From,
       string To,
       DateTimeOffset Departure,
       DateTimeOffset Arrival);
}
