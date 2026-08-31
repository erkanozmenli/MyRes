namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation.Contracts
{
    public sealed record FlightSegmentInput(
       string From,
       string To,
       DateTimeOffset Departure,
       DateTimeOffset Arrival);
}
