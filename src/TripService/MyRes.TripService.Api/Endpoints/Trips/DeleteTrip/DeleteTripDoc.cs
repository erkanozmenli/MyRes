using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.DeleteTrip
{
    public class DeleteTripDoc
    {
        public static readonly EndpointDocumentation Metadata =
            new(
                Summary: "Delete trip by id",
                Description:
"""
Deletes trip using its unique identifier.
"""
            );
    }
}
