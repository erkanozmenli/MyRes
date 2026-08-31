using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.BuildingBlocks.Authentication;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;
using MyRes.TripService.Application.Trips.Queries.Shared.Projections;

namespace MyRes.TripService.Application.Trips.Queries.GetTripsByUserId
{
    internal class GetTripsByUserIdHandler
        (IFlightReservationQueryService flightReservationQueryService, ITripQueryService tripQueryService, ICurrentIdentityAccessor currentIdentityAccessor)
        : IQueryHandler<GetTripsByUserIdQuery, GetTripsByUserIdResult>
    {
        public async Task<GetTripsByUserIdResult> Handle(GetTripsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = currentIdentityAccessor.Identity.UserId ?? throw new UnauthorizedAccessException("An authenticated user is required to retrieve trips.");

            var trips = await tripQueryService.GetTripsByUserIdAsync(userId);

            if (!trips.Any())
                throw new TripsNotFoundForUserException(userId);

            foreach (var trip in trips)
            {
                // flight reservations
                var flatFlightReservations = await flightReservationQueryService.GetFlightReservationsByTripIdAsync(trip.Id);
                var flightReservations = TripItemAssembler.ToFlightReservations(flatFlightReservations);
                trip.TripItems.AddRange(flightReservations);
            }

            return new GetTripsByUserIdResult(trips);
        }
    }
}
