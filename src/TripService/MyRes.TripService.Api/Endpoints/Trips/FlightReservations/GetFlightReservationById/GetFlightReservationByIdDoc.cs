using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationById
{
    public class GetFlightReservationByIdDoc
    {
        public static readonly EndpointDocumentation Metadata = new(
            Summary: "Gets flight reservation by Id",
            Description:
"""
Gets flight reservation by Id.
"""
    );
    }
}
