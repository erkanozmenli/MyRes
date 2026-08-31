using MyRes.BuildingBlocks.Exceptions;
using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Domain.Exceptions
{
    public class UnexpectedTripStatusException : DomainException
    {
        public UnexpectedTripStatusException(TripStatus status, TripStatus expectedTripStatus)
            : base(
                  "trip.unexpected_trip_status",
                  $"Expected status '{expectedTripStatus}' but was '{status}'.")
        {

        }
    }
}
