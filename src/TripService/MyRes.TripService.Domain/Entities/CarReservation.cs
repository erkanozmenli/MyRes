namespace MyRes.TripService.Domain.Entities
{
    public class CarReservation : TripItem
    {
        public string CarBrand { get; private set; } = null!;
        public string CarModel { get; private set; } = null!;
        public DateTimeOffset PickupDate { get; private set; }
        public DateTimeOffset ReturnDate { get; private set; }

        private CarReservation()
        {

        }

        public CarReservation(string carBrand, string carModel, DateTimeOffset pickupDate, DateTimeOffset returnDate)
        {
            CarBrand = carBrand;
            CarModel = carModel;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
        }
    }
}
