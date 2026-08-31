using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Trips.Commands.AddFlightReservation.DTOs;

namespace MyRes.TripService.Application.Trips.Commands.AddFlightReservation
{
    public record AddFlightReservationCommand(
        Guid TripId,
        FlightReservationDto FlightReservation) : ICommand<AddFlightReservationResult>;
}
