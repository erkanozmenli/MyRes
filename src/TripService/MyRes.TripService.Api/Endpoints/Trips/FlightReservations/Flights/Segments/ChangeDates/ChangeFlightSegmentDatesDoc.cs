using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.Flights.Segments.ChangeDates
{
    public class ChangeFlightSegmentDatesDoc
    {
        public static readonly EndpointDocumentation Metadata =
            new(
                Summary: "Changes flight reservation's segment dates.",
                Description:
"""
Changes flight reservation's segment dates by segment id.
"""
            );
    }
}
