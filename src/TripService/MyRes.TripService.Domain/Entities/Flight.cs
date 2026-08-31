using MyRes.TripService.Domain.Enums;
using MyRes.Shared.Domain;
using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.Entities
{
    public class Flight : Entity<int>
    {
        public int TripItemId { get; private set; }

        public FlightDirection Direction { get; private set; }

        private readonly List<FlightSegment> _segments = new();
        public IReadOnlyCollection<FlightSegment> Segments => _segments.AsReadOnly();


        private Flight()
        {

        }

        public static Flight Create(FlightDirection direction, IEnumerable<FlightSegment> segments)
        {
            var flight = new Flight();

            flight.Direction = direction;
            flight._segments.AddRange(segments);

            if (!flight._segments.Any())
                throw new FlightMustHaveAtLeastOneSegmentException();

            return flight;
        }
    }
}
