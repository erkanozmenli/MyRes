using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class FlightDoesNotExistException : DomainException
    {
        public FlightDoesNotExistException(int id)
            : base(
                  "trip.flightreservation.flight.does_not_exist",
                  $"Flight with Id:{id} does not exist in flight reservation")
        {

        }
    }
}
