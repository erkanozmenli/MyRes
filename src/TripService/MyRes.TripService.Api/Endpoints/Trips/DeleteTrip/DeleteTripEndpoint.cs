using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Commands.DeleteTrip;


namespace MyRes.TripService.Api.Endpoints.Trips.DeleteTrip
{
    public class DeleteTripEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Commands")
                .WithGroupName("v1");

            group.MapDelete("/{tripId:Guid}", async (Guid tripId, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new DeleteTripCommand(tripId));
                return result.IsSuccess ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteTrip")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithDoc(DeleteTripDoc.Metadata);
        }
    }
}
