using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.Trips.Queries.Shared.DTOs
{
    public record FlightReservationDto : TripItemDto
    {
        public TripType TripType { get; init; }
        public IReadOnlyList<FlightDto> Flights { get; init; } = [];
    }
}
