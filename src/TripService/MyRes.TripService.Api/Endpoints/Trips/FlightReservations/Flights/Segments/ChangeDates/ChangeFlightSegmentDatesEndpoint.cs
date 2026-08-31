using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Commands.ChangeFlightSegmentDates;
using MyRes.TripService.Application.Trips.Commands.ChangeFlightSegmentDates.DTOs;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.Flights.Segments.ChangeDates
{
    public class ChangeFlightSegmentDatesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Commands")
                .WithGroupName("v1");

            group.MapPut("/{tripId:Guid}/flight-reservations/{flightReservationId:int}/flights/{flightId:int}/segments/{segmentId:int}/change-dates", async (Guid tripId, int flightReservationId, int flightId, int segmentId, ChangeFlightSegmentDatesRequest request, [FromServices] ISender sender) =>
            {
                var dto = request.Adapt<ChangeFlightSegmentDatesDto>();

                var result = await sender.Send(new ChangeFlightSegmentDatesCommand(tripId, flightReservationId, flightId, segmentId, dto));

                return result.IsSuccess ? Results.NoContent() : Results.NotFound();
            })
            .WithName("ChangeFlightSegmentDates")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithDoc(ChangeFlightSegmentDatesDoc.Metadata);
        }
    }
}
