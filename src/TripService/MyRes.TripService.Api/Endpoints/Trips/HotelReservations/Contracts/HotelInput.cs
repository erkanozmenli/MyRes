namespace MyRes.TripService.Api.Endpoints.Trips.HotelReservations.Contracts
{
    public sealed record HotelInput(
        string HotelName,
        DateTimeOffset CheckIn,
        DateTimeOffset CheckOut,
        int Guests);
}
