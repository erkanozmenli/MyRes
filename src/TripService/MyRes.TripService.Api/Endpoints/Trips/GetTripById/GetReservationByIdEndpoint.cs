using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Queries.GetTripById;


namespace MyRes.TripService.Api.Endpoints.Trips.GetById
{
    public class GetTripByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Queries")
                .WithGroupName("v1");

            group.MapGet("/{tripId:Guid}", async (Guid tripId, [FromServices] ISender sender) =>
            {
                var reservation = await sender.Send(new GetTripByIdQuery(tripId));

                return Results.Ok(new GetTripByIdResponse(reservation.Trip));
            })
            .WithName("GetTripById")
            .Produces<GetTripByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDoc(GetTripByIdDocs.Metadata);
        }
    }
}
