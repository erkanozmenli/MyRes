using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Commands.AddFlightReservation;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation
{
    public class AddFlightReservationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Commands")
                .WithGroupName("v1");

            group.MapPost("/{tripId:guid}/flight-reservations", async (Guid tripId, AddFlightReservationRequest request, [FromServices] ISender sender) =>
            {
                var command = request.Adapt<AddFlightReservationCommand>() with { TripId = tripId };

                var result = await sender.Send(command);

                var response = result.Adapt<AddFlightReservationResponse>();

                return Results.Created($"/trips/{response.TripId}/flight-reservations/{response.FlightReservationId}/", response);
            })
            .WithName("AddFlightReservation")
            .Produces<AddFlightReservationResult>(StatusCodes.Status201Created)
            .WithDoc(AddFlightReservationDoc.Metadata);
        }
    }
}
