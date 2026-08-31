using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class GuestMustBeAtLeastOneException : DomainException
    {
        public GuestMustBeAtLeastOneException() :
            base(
                    "trip.hotelreservation.guests_must_be_at_least_one",
                    "Guests must be at least one."
                )
        {

        }
    }
}
