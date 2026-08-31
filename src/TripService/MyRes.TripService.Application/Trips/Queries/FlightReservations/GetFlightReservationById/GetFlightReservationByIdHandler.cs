using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;
using MyRes.TripService.Application.Queries.Shared.Projections;


namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationById
{
    internal class GetFlightReservationByIdHandler
        (IFlightReservationQueryService flightReservationQueryService) : IQueryHandler<GetFlightReservationByIdQuery, GetFlightReservationByIdResult>
    {
        public async Task<GetFlightReservationByIdResult> Handle(GetFlightReservationByIdQuery query, CancellationToken cancellationToken)
        {
            var flightResult = await flightReservationQueryService.GetFlightReservationByIdAsync(query.TripId, query.Id);

            if (!flightResult.Any())
                throw new FlightReservationNotFoundException(query.Id);

            var flightReservation = flightResult.ToDto();

            return new GetFlightReservationByIdResult(flightReservation);
        }
    }
}
