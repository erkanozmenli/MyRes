namespace MyRes.TripService.Application.Trips.Commands.ChangeFlightSegmentDates.DTOs
{
    public record ChangeFlightSegmentDatesDto(DateTimeOffset DepartureDate, DateTimeOffset ArrivalDate);
}
