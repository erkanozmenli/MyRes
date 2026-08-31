using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.BuildingBlocks.Authentication;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;
using MyRes.TripService.Application.Trips.Queries.Shared.Projections;

namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByUserId
{
    internal sealed class GetFlightReservationsByUserIdHandler
        (IFlightReservationQueryService flightReservationQueryService, ICurrentIdentityAccessor currentIdentityAccessor)
        : IQueryHandler<GetFlightReservationsByUserIdQuery, GetFlightReservationsByUserIdResult>
    {
        public async Task<GetFlightReservationsByUserIdResult> Handle(GetFlightReservationsByUserIdQuery query, CancellationToken cancellationToken)
        {
            var userId = currentIdentityAccessor.Identity.UserId ?? throw new UnauthorizedAccessException("An authenticated user is required to retrieve flight reservations.");

            var flatFlightReservations = await flightReservationQueryService.GetFlightReservationsByUserIdAsync(userId);

            if (!flatFlightReservations.Any())
                throw new FlightReservationsNotFoundForUserException(userId);

            var flightReservations = TripItemAssembler.ToFlightReservations(flatFlightReservations);

            return new GetFlightReservationsByUserIdResult(flightReservations);
        }
    }
}
