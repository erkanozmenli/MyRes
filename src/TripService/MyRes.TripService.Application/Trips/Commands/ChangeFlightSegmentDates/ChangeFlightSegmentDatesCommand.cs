using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Trips.Commands.ChangeFlightSegmentDates.DTOs;

namespace MyRes.TripService.Application.Trips.Commands.ChangeFlightSegmentDates
{
    public record ChangeFlightSegmentDatesCommand(Guid TripId, int FlightReservationId, int FlightId, int SegmentId, ChangeFlightSegmentDatesDto FlightSegmentDates) : ICommand<ChangeFlightSegmentDatesResult>;
}
