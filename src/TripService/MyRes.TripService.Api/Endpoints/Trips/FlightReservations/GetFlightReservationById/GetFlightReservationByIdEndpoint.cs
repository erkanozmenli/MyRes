using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationById;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationById
{
    public class GetFlightReservationByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Queries")
                .WithGroupName("v1");

            group.MapGet("/{tripItemId:guid}/flight-reservations/{id:int}", async (Guid tripItemId, int id, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetFlightReservationByIdQuery(tripItemId, id));

                return Results.Ok(new GetFlightReservationByIdResponse(result.FlightReservation));
            })
            .WithName("GetFlightReservationById")
            .Produces<GetFlightReservationByIdResponse>()
            .WithDoc(GetFlightReservationByIdDoc.Metadata);
        }
    }
}
