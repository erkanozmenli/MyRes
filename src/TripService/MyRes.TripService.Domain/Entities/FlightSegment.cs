using MyRes.Shared.Domain;
using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.Entities
{
    public class FlightSegment : Entity<int>
    {
        public int FlightId { get; private set; }

        // Self-reference
        public int? PreviousSegmentId { get; private set; }
        public FlightSegment? PreviousSegment { get; private set; }


        public string FromAirport { get; private set; } = null!;
        public string ToAirport { get; private set; } = null!;
        public DateTimeOffset DepartureTime { get; private set; }
        public DateTimeOffset ArrivalTime { get; private set; }

        private FlightSegment()
        {

        }

        public static FlightSegment Create(string from, string to, DateTimeOffset departure, DateTimeOffset arrival, FlightSegment? previous = null)
        {
            var segment = new FlightSegment
            {
                FromAirport = from,
                ToAirport = to,
                PreviousSegment = previous,
                PreviousSegmentId = previous?.Id
            };

            segment.ChangeDates(departure, arrival);

            return segment;
        }

        public void ChangeDates(DateTimeOffset departureTime, DateTimeOffset arrivalTime)
        {
            if (departureTime > arrivalTime)
                throw new ArrivalTimeMustBeAfterDepartureTimeException();

            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }
    }
}
