using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Application.Exceptions
{
    public class FlightReservationNotFoundException : NotFoundException
    {
        public FlightReservationNotFoundException(int Id) : base("Flight Reservation for id", Id)
        {
        }
    }
}
