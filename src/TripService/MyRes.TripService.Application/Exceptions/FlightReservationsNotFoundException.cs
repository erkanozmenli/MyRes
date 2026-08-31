using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Application.Exceptions
{
    public class FlightReservationsNotFoundException : NotFoundException
    {
        public FlightReservationsNotFoundException(Guid tripId) : base("Flight Reservations for trip id", tripId)
        {
        }
    }
}
