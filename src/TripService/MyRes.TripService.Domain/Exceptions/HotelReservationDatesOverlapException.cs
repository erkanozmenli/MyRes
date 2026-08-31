using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class HotelReservationDatesOverlapException : DomainException
    {
        public DateTimeOffset ExistingCheckIn { get; }
        public DateTimeOffset ExistingCheckOut { get; }
        public DateTimeOffset NewCheckIn { get; }
        public DateTimeOffset NewCheckOut { get; }

        public HotelReservationDatesOverlapException(
            DateTimeOffset existingCheckIn,
            DateTimeOffset existingCheckOut,
            DateTimeOffset newCheckIn,
            DateTimeOffset newCheckOut
            ) : base(
                    "trip.hotelReservation.overlap",
                    $"Hotel reservation dates overlap with an existing reservation. " +
                    $"Existing: {existingCheckIn:yyyy-MM-dd} - {existingCheckOut:yyyy-MM-dd}, " +
                    $"New: {newCheckIn:yyyy-MM-dd} - {newCheckOut:yyyy-MM-dd}")
        {
            ExistingCheckIn = existingCheckIn;
            ExistingCheckOut = existingCheckOut;
            NewCheckIn = newCheckIn;
            NewCheckOut = newCheckOut;
        }
    }
}
