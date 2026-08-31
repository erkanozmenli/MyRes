using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class OneWayTripMustHaveExactlyOneOutboundFlightException : DomainException
    {
        public OneWayTripMustHaveExactlyOneOutboundFlightException()
            : base(
                  "trip.flightReservation.one_way_trip_must_have_exactly_one_outbound_flight",
                  "One-way trip must have exactly one outbound flight"
                  )
        {

        }
    }
}
