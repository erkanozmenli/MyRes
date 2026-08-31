using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Application.Exceptions
{
    public class TripsNotFoundForUserException : NotFoundException
    {
        public TripsNotFoundForUserException(Guid userId) : base("Trips for user", userId)
        {
        }
    }
}
