using MyRes.TripService.Application.Queries.Shared.Models;
using MyRes.TripService.Application.Queries.Shared.Projections;
using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;

namespace MyRes.TripService.Application.Trips.Queries.Shared.Projections
{
    internal class TripItemAssembler
    {
        internal static IReadOnlyList<FlightReservationDto> ToFlightReservations(IEnumerable<FlightReservationFlatRow> rows)
        {
            return rows
                .GroupBy(x => x.TripItemId)
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
