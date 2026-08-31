namespace MyRes.TripService.Application.Trips.Queries.Shared.DTOs
{
    public record TripDto
    {
        public Guid Id { get; init; }
        public int TripNo { get; init; }
        public List<TripItemDto> TripItems { get; init; } = [];
    }
}
