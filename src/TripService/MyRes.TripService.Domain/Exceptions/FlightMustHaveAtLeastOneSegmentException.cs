using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class FlightMustHaveAtLeastOneSegmentException : DomainException
    {
        public FlightMustHaveAtLeastOneSegmentException() :
            base(
                "trip.flightreservation.flight.at_least_one_segment",
                "Flight must have at least one segment exception")
        {

        }
    }
}
