using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Api.Endpoints.Trips.GetById;
using MyRes.TripService.Application.Trips.Queries.GetTripsByUserId;

namespace MyRes.TripService.Api.Endpoints.Trips.GetTripsByUserId
{
    public class GetTripsByUserIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Queries")
                .WithGroupName("v1");

            group.MapGet("/", async ([FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetTripsByUserIdQuery());

                return Results.Ok(new GetTripsByUserIdResponse(result.Trips));
            })
            .WithName("GetTripsByUserId")
            .Produces<GetTripByIdResponse>(StatusCodes.Status200OK)
            .WithDoc(GetTripsByUserIdDocs.Metadata);
        }
    }
}
