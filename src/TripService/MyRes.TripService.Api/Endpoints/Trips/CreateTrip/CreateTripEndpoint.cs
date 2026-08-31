using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRes.BuildingBlocks.Api.OpenApi;
using MyRes.TripService.Application.Trips.Commands.CreateTrip;


namespace MyRes.TripService.Api.Endpoints.Trips.CreateTrip
{
    public class CreateTripEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/trips")
                .WithTags("Trips/Commands")
                .WithGroupName("v1");

            group.MapPost("/", async (CreateTripRequest request, [FromServices] ISender sender) =>
            {
                var command = request.Adapt<CreateTripCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateTripResponse>();

                return Results.Created($"/v1/trips/{response.Id}", response);
            })
            .WithName("CreateTrip")
            .Produces<CreateTripResponse>(StatusCodes.Status201Created)
            .WithDoc(CreateTripDoc.Metadata);
        }
    }
}
