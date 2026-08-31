using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.CreateTrip
{
    public static class CreateTripDoc
    {
        public static readonly EndpointDocumentation Metadata =
            new(
                Summary: "Create Trip",
                Description:
"""
Creates a new trip.
"""
            );
    }
}
