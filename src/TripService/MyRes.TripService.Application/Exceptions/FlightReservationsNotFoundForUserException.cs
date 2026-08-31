using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Application.Exceptions
{
    public class FlightReservationsNotFoundForUserException : NotFoundException
    {
        public FlightReservationsNotFoundForUserException(Guid userId) : base("Flights for user", userId)
        {
        }
    }
}
