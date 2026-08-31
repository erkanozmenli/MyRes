using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class FlightSegmentDoesNotExistException : DomainException
    {
        public FlightSegmentDoesNotExistException(int id)
            : base(
                  "trip.flightreservation.flight.flightSegment.does_not_exist",
                  $"Flight segment with Id:{id} does not exist in flight")
        {

        }
    }
}
