using Microsoft.EntityFrameworkCore;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Queries.Shared.Models;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.TripService.Infrastructure.Data.QueryService.FlightReservation
{
    internal class FlightReservationQueryService(IDapperExecuter dapperExecuter, TripDbContext tripsDbContext) : IFlightReservationQueryService
    {
        #region EF Core

        public async Task<bool> ExistsAsync(Guid tripId, int flightReservationId)
        {
            return await tripsDbContext.FlightReservations
                .AsNoTracking()
                .AnyAsync(x =>
                    x.TripId == tripId && x.Id == flightReservationId
                );
        }

        #endregion


        #region Dapper

        public async Task<IReadOnlyList<FlightReservationFlatRow>> GetFlightReservationByIdAsync(Guid tripId, int Id)
        {
            const string sql = "uspGetFlightReservationById";
            return await dapperExecuter.QueryAsync<FlightReservationFlatRow>(sql, new { TripId = tripId, Id = Id }, System.Data.CommandType.StoredProcedure);
        }

        public async Task<IReadOnlyList<FlightReservationFlatRow>> GetFlightReservationsByTripIdAsync(Guid tripId)
        {
            const string sql = "uspGetFlightReservationsByTripId";
            return await dapperExecuter.QueryAsync<FlightReservationFlatRow>(sql, new { TripId = tripId }, System.Data.CommandType.StoredProcedure);
        }

        public async Task<IReadOnlyList<FlightReservationFlatRow>> GetFlightReservationsByUserIdAsync(Guid userId)
        {
            const string sql = "uspGetFlightReservationsByUserId";
            return await dapperExecuter.QueryAsync<FlightReservationFlatRow>(sql, new { UserId = userId }, System.Data.CommandType.StoredProcedure);
        }

        #endregion
    }
}
