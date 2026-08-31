using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.Entities
{
    public class HotelReservation : TripItem
    {
        public string HotelName { get; private set; } = null!;
        public DateTimeOffset CheckIn { get; private set; }
        public DateTimeOffset CheckOut { get; private set; }
        public int Guests { get; private set; }

        private HotelReservation() { }

        public static HotelReservation Create(string hotelName, DateTimeOffset checkIn, DateTimeOffset checkOut, int guests)
        {
            var hotelReservation = new HotelReservation();

            hotelReservation.ValidateDates(checkIn, checkOut);
            hotelReservation.ValidateGuests(guests);

            hotelReservation = new HotelReservation
            {
                HotelName = hotelName,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Guests = guests
            };

            return hotelReservation;
        }

        internal void ChangeDates(DateTimeOffset newCheckIn, DateTimeOffset newCheckOut)
        {
            ValidateDates(newCheckIn, newCheckOut);

            CheckIn = newCheckIn;
            CheckOut = newCheckOut;
        }

        private void ValidateDates(DateTimeOffset checkIn, DateTimeOffset checkOut)
        {
            if (checkOut <= checkIn)
                throw new CheckoutMustBeAfterCheckinException();
        }

        private void ValidateGuests(int guests)
        {
            if (guests <= 0)
                throw new GuestMustBeAtLeastOneException();
        }
    }
}
