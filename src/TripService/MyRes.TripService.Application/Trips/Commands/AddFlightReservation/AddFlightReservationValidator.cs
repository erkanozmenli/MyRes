using FluentValidation;
using MyRes.TripService.Application.Trips.Commands.AddFlightReservation.DTOs;

namespace MyRes.TripService.Application.Trips.Commands.AddFlightReservation
{
    public class AddFlightReservationValidator : AbstractValidator<AddFlightReservationCommand>
    {
        public AddFlightReservationValidator()
        {
            RuleFor(x => x.FlightReservation)
                .NotNull()
                .WithErrorCode("flightreservation.required");

            RuleFor(x => x.FlightReservation.TripType)
                .IsInEnum()
                .WithErrorCode("flightreservation.triptype_invalid");

            RuleFor(x => x.FlightReservation.Flights)
                .NotEmpty()
                .WithMessage("At least one flight is required")
                .WithErrorCode("flightreservation.flights_required");

            RuleForEach(x => x.FlightReservation.Flights)
                .SetValidator(new FlightDtoValidator());
        }
    }

    public class FlightDtoValidator : AbstractValidator<FlightDto>
    {
        public FlightDtoValidator()
        {
            RuleFor(x => x.Direction)
                .IsInEnum()
                .WithErrorCode("flight.direction_invalid");

            RuleFor(x => x.Segments)
                .NotEmpty()
                .WithErrorCode("flight.segments_required");

            RuleForEach(x => x.Segments)
                .SetValidator(new FlightSegmentDtoValidator());
        }
    }

    public class FlightSegmentDtoValidator : AbstractValidator<FlightSegmentDto>
    {
        public FlightSegmentDtoValidator()
        {
            RuleFor(x => x.From)
                .NotEmpty()
                .WithErrorCode("flight.segment.from_required")
                .Length(3)
                .WithErrorCode("flight.segment.from_invalid_length")
                .WithMessage("Departure airport must be a valid IATA code.");

            RuleFor(x => x.To)
                .NotEmpty()
                .WithErrorCode("flight.segment.to_required")
                .Length(3)
                .WithErrorCode("flight.segment.to_invalid_length")
                .WithMessage("Arrival airport must be a valid IATA code.");

            RuleFor(x => x.Departure)
                .NotEmpty()
                .WithErrorCode("flight.segment.departure_required");

            RuleFor(x => x.Arrival)
                .NotEmpty()
                .WithErrorCode("flight.segment.arrival_required");

            RuleFor(x => x)
                .Must(x => x.Arrival > x.Departure)
                .WithErrorCode("flight.segment.date_invalid")
                .WithMessage("Arrival date must be after departure date.");
        }
    }
}
