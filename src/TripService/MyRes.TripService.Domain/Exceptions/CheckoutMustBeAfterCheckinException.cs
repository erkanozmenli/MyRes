using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class CheckoutMustBeAfterCheckinException : DomainException
    {
        public CheckoutMustBeAfterCheckinException() :
            base(
                    "trip.hotelreservation.checkout_must_be_after_check_in",
                    "Check-out must be after check-in"
                )
        {

        }
    }
}
