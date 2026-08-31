using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.BuildingBlocks.Authentication;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Domain.Entities.AggregateRoots;

namespace MyRes.TripService.Application.Trips.Commands.CreateTrip
{
    internal class CreateTripHandler
        (ITripRepository tripRepository, ICurrentIdentityAccessor accessor)
        : ICommandHandler<CreateTripCommand, CreateTripResult>
    {
        public async Task<CreateTripResult> Handle(CreateTripCommand command, CancellationToken cancellationToken)
        {
            var reservation = Trip.Create(command.Trip.Note, accessor.Identity.UserId!.Value);

            await tripRepository.AddAsync(reservation);
            await tripRepository.SaveChangesAsync();

            return new CreateTripResult(reservation.Id);
        }
    }
}
