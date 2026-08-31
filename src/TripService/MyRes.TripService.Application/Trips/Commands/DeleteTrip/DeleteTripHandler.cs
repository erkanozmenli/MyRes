using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Abstractions;

namespace MyRes.TripService.Application.Trips.Commands.DeleteTrip
{
    public class DeleteTripHandler
        (ITripRepository tripRepository)
        : ICommandHandler<DeleteTripCommand, DeleteTripResult>
    {
        public async Task<DeleteTripResult> Handle(DeleteTripCommand command, CancellationToken cancellationToken)
        {
            var reservation = await tripRepository.GetByGuidIdAsync(command.TripId);

            if (reservation is null)
                return new DeleteTripResult(false);

            tripRepository.Remove(reservation);
            await tripRepository.SaveChangesAsync();

            return new DeleteTripResult(true);
        }
    }
}
