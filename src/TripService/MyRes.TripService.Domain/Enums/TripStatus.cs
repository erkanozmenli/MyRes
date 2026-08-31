namespace MyRes.TripService.Domain.Enums
{
    public enum TripStatus
    {
        Draft = 0,
        CheckoutPending = 1,
        PaymentCompleted = 2,
        BookingCompleted = 3,
        PaymentFailed = 4,
        BookingFailed = 5,
        //RefundPending = 6,
        Refunded = 7
    }
}
