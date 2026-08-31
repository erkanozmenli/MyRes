using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.Queries.Shared.Models
{
    public record FlightReservationFlatRow(
        Guid TripId,
        int TripNo,
        int TripItemId,
        int FlightId,
        int FlightSegmentId,
        int? PreviousSegmentId,
        TripType TripType,
        int Direction,
        string FromAirport,
        string ToAirport,
        DateTimeOffset DepartureTime,
        DateTimeOffset ArrivalTime,
        Guid UserId
        );
}
