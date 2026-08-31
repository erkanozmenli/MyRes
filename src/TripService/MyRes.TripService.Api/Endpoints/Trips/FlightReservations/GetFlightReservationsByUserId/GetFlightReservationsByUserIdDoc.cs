using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationsByUserId
{
    public static class GetFlightReservationsByUserIdDoc
    {
        public static readonly EndpointDocumentation Metadata = new(
            Summary: "Gets flight reservations for the current user",
            Description:
            """
            Retrieves all flight reservations that belong to the authenticated user.
            """);
    }
}
