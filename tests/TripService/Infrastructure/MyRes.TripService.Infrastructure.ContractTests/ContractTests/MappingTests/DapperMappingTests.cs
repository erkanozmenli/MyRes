using Dapper;
using MyRes.TripService.Application.Queries.Shared.Models;
using MyRes.TripService.Infrastructure.ContractTests.Common.Fixtures;
using MyRes.TripService.Infrastructure.ContractTests.ContractTests.Data;
using System.Data;
using Xunit.Abstractions;

namespace MyRes.TripService.Infrastructure.ContractTests.ContractTests.MappingTests
{
    [Collection(nameof(DatabaseCollection))]
    public class DapperMappingTests : IAsyncLifetime
    {
        private readonly TripsDatabaseFixture _fixture;
        private readonly ITestOutputHelper _outputHelper;


        public DapperMappingTests(TripsDatabaseFixture fixture, ITestOutputHelper outputHelper)
        {
            _fixture = fixture;
            _outputHelper = outputHelper;
        }

        public async Task InitializeAsync()
        {
            await _fixture.ResetDatabaseAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task FlightReservationFlatRow_Should_Match_SP_Output()
        {
            _outputHelper.WriteLine($"Test Container DB Conn String: {_fixture.ConnectionString}");

            // Arrange
            await using var connection = _fixture.CreateConnection();
            await connection.OpenAsync();

            await using var dbContext = _fixture.CreateDbContext();

            await dbContext.Trips.AddRangeAsync(TripData.Trips);
            await dbContext.SaveChangesAsync();


            // Act
            var testId = await connection.QuerySingleAsync<Guid>(@"SELECT TOP 1 TripId FROM vwFlight_Reservation");

            var sql = "uspGetFlightReservationsByTripId";

            var rows = await connection.QueryAsync(
                    sql,
                    new { TripId = testId },
                    commandType: CommandType.StoredProcedure
                );

            var row = (IDictionary<string, object>)rows.First();


            // Assert
            MappingAssertions.AssertMapping<FlightReservationFlatRow>(row);
        }
    }
}
