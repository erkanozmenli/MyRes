using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Api.Endpoints.Trips.CreateTrip;
using MyRes.TripService.Application.Trips.Commands.CheckoutTrip;

namespace MyRes.TripService.Api.Endpoints.Trips.CheckoutTrip
{
    public class CheckoutTripEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Commands")
                .WithGroupName("v1");

            group.MapPost("/{tripId:Guid}/checkout", async (Guid tripId, [FromServices] ISender sender) =>
            {
                var command = new CheckoutTripCommand(tripId);

                var result = await sender.Send(command);

                var response = result.Adapt<CheckoutTripResponse>();

                return Results.Accepted($"/v1/trips/checkout/{response.Id}", response);
            })
            .WithName("ChekoutTrip")
            .Produces<CreateTripResponse>(StatusCodes.Status202Accepted)
            .WithDoc(CheckoutTripDoc.Metadata);
        }
    }
}
