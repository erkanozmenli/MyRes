using Microsoft.Extensions.Logging;
using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Abstractions.Metrics;
using MyRes.TripService.Application.Exceptions;
using MyRes.TripService.Application.Trips.Queries.Shared.Projections;


namespace MyRes.TripService.Application.Trips.Queries.GetTripById
{
    internal class GetTripByIdHandler
        (IFlightReservationQueryService flightReservationQueryService, ITripQueryService tripQueryService, ILogger<GetTripByIdHandler> logger, ITripMetrics tripMetrics)
        : IQueryHandler<GetTripByIdQuery, GetTripByIdResult>
    {
        public async Task<GetTripByIdResult> Handle(GetTripByIdQuery query, CancellationToken cancellationToken)
        {
            var trip = await tripQueryService.GetTripByIdAsync(query.TripId);

            if (trip is null)
                throw new TripNotFoundException(query.TripId);

            // flight reservations
            var flatFlightReservations = await flightReservationQueryService.GetFlightReservationsByTripIdAsync(query.TripId);
            var flightReservations = TripItemAssembler.ToFlightReservations(flatFlightReservations);

            trip.TripItems.AddRange(flightReservations);


            tripMetrics.TripRetrieved(query.TripId, trip.TripItems.Count);
            logger.LogInformation("Trip retrieved successfully. {@Trip}", trip);

            return new GetTripByIdResult(trip);
        }
    }
}
