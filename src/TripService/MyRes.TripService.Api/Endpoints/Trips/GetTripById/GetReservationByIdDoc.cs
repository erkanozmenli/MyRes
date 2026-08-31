using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.GetById
{
    public static class GetTripByIdDocs
    {
        public static readonly EndpointDocumentation Metadata =
            new(
                Summary: "Get trip by id",
                Description:
"""
Retrieves a trip using its unique identifier.
"""
            );
    }
}
