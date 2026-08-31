namespace MyRes.TripService.Application.Trips.Commands.AddHotelReservation.DTOs
{
    public sealed record HotelDto(
        string HotelName,
        DateTimeOffset CheckIn,
        DateTimeOffset CheckOut,
        int Guests);
}
