using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;
using MyRes.TripService.Application.Trips.Queries.Shared.Projections;

namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByTripId
{
    internal class GetFlightReservationsByTripIdHandler
       (IFlightReservationQueryService flightReservationQueryService) : IQueryHandler<GetFlightReservationsByTripIdQuery, GetFlightReservationsByTripIdResult>
    {
        public async Task<GetFlightReservationsByTripIdResult> Handle(GetFlightReservationsByTripIdQuery query, CancellationToken cancellationToken)
        {
            var flatFlightReservations = await flightReservationQueryService.GetFlightReservationsByTripIdAsync(query.TripId);

            if (!flatFlightReservations.Any())
                throw new FlightReservationsNotFoundException(query.TripId);

            var flightReservations = TripItemAssembler.ToFlightReservations(flatFlightReservations);
            return new GetFlightReservationsByTripIdResult(flightReservations);
        }
    }
}
