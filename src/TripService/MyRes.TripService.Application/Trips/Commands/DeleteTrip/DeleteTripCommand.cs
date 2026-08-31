using MyRes.BuildingBlocks.Application.CQRS;

namespace MyRes.TripService.Application.Trips.Commands.DeleteTrip
{
    public record DeleteTripCommand(Guid TripId) : ICommand<DeleteTripResult>;
}
