using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByTripId;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationsByTripId
{
    public class GetFlightReservationsByTripIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Queries")
                .WithGroupName("v1");

            group.MapGet("/{tripId:guid}/flight-reservations", async (Guid tripId, [AsParameters] GetFlightReservationsByTripIdRequest request, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(request.Adapt<GetFlightReservationsByTripIdQuery>() with { TripId = tripId });

                return Results.Ok(new GetFlightReservationsByTripIdResponse(result.TripItems));
            })
            .WithName("GetFlightReservations")
            .Produces<GetFlightReservationsByTripIdResponse>()
            .WithDoc(GetFlightReservationsByTripIdDoc.Metadata);
        }
    }
}
