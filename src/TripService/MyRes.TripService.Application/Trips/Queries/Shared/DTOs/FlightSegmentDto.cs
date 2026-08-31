namespace MyRes.TripService.Application.Trips.Queries.Shared.DTOs
{
    public record FlightSegmentDto
    {
        public int Id { get; init; }
        public int? PreviousSegmentId { get; init; }
        public string From { get; init; } = null!;
        public string To { get; init; } = null!;
        public DateTimeOffset Departure { get; init; }
        public DateTimeOffset Arrival { get; init; }
    }
}
