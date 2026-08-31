using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.GetTripsByUserId
{
    public static class GetTripsByUserIdDocs
    {
        public static readonly EndpointDocumentation Metadata =
            new(
                Summary: "Get trips by user id",
                Description:
"""
Retrieves trips using user's unique identifier.
"""
            );
    }
}
