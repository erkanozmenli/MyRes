using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.Trips.Queries.Shared.DTOs
{
    public record FlightDto
    {
        public FlightDirection Direction { get; init; }
        public IReadOnlyList<FlightSegmentDto> Segments { get; init; } = [];
    }
}
