using FluentValidation;

namespace MyRes.TripService.Application.Trips.Commands.DeleteTrip
{
    public class DeleteTripValidator : AbstractValidator<DeleteTripCommand>
    {
        public DeleteTripValidator()
        {
            RuleFor(x => x.TripId)
                .NotEmpty()
                .WithErrorCode("trip.id_required");
        }
    }
}
