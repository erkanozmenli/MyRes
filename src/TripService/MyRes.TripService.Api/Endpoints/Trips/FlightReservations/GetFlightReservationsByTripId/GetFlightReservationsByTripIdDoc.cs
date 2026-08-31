using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationsByTripId
{
    public class GetFlightReservationsByTripIdDoc
    {
        public static readonly EndpointDocumentation Metadata = new(
    Summary: "Gets flight reservations by TripId",
    Description:
"""
Gets flight reservations by TripId.
"""
);
    }
}
