using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation
{
    public class AddFlightReservationDoc
    {
        public static readonly EndpointDocumentation Metadata =
    new(
        Summary: "Add Flight Reservation",
        Description:
"""
Adds a new flight reservation to an existing Trip.
"""
    );
    }
}
