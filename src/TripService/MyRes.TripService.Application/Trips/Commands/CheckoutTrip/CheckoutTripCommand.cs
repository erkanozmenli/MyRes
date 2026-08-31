using MyRes.BuildingBlocks.Application.CQRS;

namespace MyRes.TripService.Application.Trips.Commands.CheckoutTrip
{
    public record CheckoutTripCommand(Guid TripId) : ICommand<CheckoutTripResult>;
}
