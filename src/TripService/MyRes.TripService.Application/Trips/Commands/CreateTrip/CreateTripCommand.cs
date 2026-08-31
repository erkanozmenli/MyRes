using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Trips.Commands.CreateTrip.DTOs;

namespace MyRes.TripService.Application.Trips.Commands.CreateTrip
{
    public record CreateTripCommand(TripDto Trip) : ICommand<CreateTripResult>;
}
