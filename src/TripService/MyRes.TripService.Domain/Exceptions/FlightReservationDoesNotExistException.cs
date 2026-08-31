using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class FlightReservationDoesNotExistException : DomainException
    {
        public FlightReservationDoesNotExistException(int id)
            : base(
                  "trip.flightReservation.does_not_exist",
                  $"Flight reservation with Id:{id} does not exist in trip")
        {

        }
    }
}
