using MyRes.BuildingBlocks.Api.OpenApi;

namespace MyRes.TripService.Api.Endpoints.Trips.CheckoutTrip
{
    public class CheckoutTripDoc
    {
        public static readonly EndpointDocumentation Metadata =
            new(
                Summary: "Chekout trip by id",
                Description:
"""
Checkouts trip using its unique identifier.
"""
            );
    }
}
