using FluentValidation;

namespace MyRes.TripService.Application.Trips.Commands.ChangeFlightSegmentDates
{
    public class ChangeFlightSegmentDatesValidator : AbstractValidator<ChangeFlightSegmentDatesCommand>
    {
        public ChangeFlightSegmentDatesValidator()
        {
            RuleFor(x => x.TripId)
            .NotEmpty()
            .WithErrorCode("trip.id_required");

            RuleFor(x => x.FlightReservationId)
                .GreaterThan(0)
                .WithErrorCode("flightreservation.id_invalid");

            RuleFor(x => x.FlightId)
                .GreaterThan(0)
                .WithErrorCode("flight.id_invalid");

            RuleFor(x => x.SegmentId)
                .GreaterThan(0)
                .WithErrorCode("flightsegment.id_invalid");

            RuleFor(x => x.FlightSegmentDates)
                .NotNull()
                .WithErrorCode("flightsegment.dates_required");

            RuleFor(x => x.FlightSegmentDates.DepartureDate)
                .NotEmpty()
                .WithErrorCode("flightsegment.departure_required");

            RuleFor(x => x.FlightSegmentDates.ArrivalDate)
                .NotEmpty()
                .WithErrorCode("flightsegment.arrival_required");

            RuleFor(x => x.FlightSegmentDates)
                .Must(x => x.ArrivalDate > x.DepartureDate)
                .WithErrorCode("flightsegment.date_range_invalid")
                .WithMessage("Arrival date must be after departure date.");
        }
    }
}
