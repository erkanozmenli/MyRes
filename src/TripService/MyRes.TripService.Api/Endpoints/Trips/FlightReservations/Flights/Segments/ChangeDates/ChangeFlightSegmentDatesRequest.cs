namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.Flights.Segments.ChangeDates
{
    public record ChangeFlightSegmentDatesRequest(DateTimeOffset DepartureDate, DateTimeOffset ArrivalDate);
}
