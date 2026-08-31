using MyRes.TripService.Api.Contracts.Enums;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation.Contracts
{
    public record FlightReservationInput(
        TripType TripType,
        IReadOnlyCollection<FlightInput> Flights);
}
