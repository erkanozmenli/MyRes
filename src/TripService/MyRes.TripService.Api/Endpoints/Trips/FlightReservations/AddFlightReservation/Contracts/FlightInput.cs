using MyRes.TripService.Api.Contracts.Enums;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation.Contracts
{
    public sealed record FlightInput(
        FlightDirection Direction,
        IReadOnlyCollection<FlightSegmentInput> Segments);
}
