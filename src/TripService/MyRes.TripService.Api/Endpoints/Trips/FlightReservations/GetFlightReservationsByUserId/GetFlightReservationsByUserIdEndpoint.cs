using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByUserId;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationsByUserId
{
    public sealed class GetFlightReservationsByUserIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Queries")
                .WithGroupName("v1");

            group.MapGet("/flight-reservations", async ([FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetFlightReservationsByUserIdQuery());

                return Results.Ok(new GetFlightReservationsByUserIdResponse(result.TripItems));
            })
            .WithName("GetFlightReservationsByUserId")
            .Produces<GetFlightReservationsByUserIdResponse>(StatusCodes.Status200OK)
            .WithDoc(GetFlightReservationsByUserIdDoc.Metadata);
        }
    }
}
