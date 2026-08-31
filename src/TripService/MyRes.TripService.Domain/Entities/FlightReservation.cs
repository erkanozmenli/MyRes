using MyRes.TripService.Domain.Enums;
using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.Entities
{
    public class FlightReservation : TripItem
    {
        public TripType TripType { get; private set; }
        private readonly List<Flight> _flights = new();
        public IReadOnlyCollection<Flight> Flights => _flights.AsReadOnly();

        private FlightReservation()
        {

        }

        public static FlightReservation Create(TripType tripType, IEnumerable<Flight> flights)
        {
            var flightReservation = new FlightReservation();
            flightReservation.TripType = tripType;
            flightReservation._flights.AddRange(flights);

            flightReservation.EnsureOutboundInbound();

            return flightReservation;
        }

        private void EnsureOutboundInbound()
        {
            var outboundCount = _flights.Count(f => f.Direction == FlightDirection.Outbound);
            var inboundCount = _flights.Count(f => f.Direction == FlightDirection.Inbound);

            if (TripType is TripType.OneWay)
            {
                if (outboundCount != 1 || inboundCount != 0)
                    throw new OneWayTripMustHaveExactlyOneOutboundFlightException();
            }

            if (TripType is TripType.RoundTrip)
            {
                if (outboundCount != 1 || inboundCount != 1)
                    throw new RoundTripRequiresInboundException();
            }
        }
    }
}
