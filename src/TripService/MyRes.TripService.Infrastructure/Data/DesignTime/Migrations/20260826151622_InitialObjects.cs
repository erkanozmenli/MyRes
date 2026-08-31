using Microsoft.EntityFrameworkCore.Migrations;
using MyRes.BuildingBlocks.Utilities;

#nullable disable

namespace MyRes.TripService.Infrastructure.Data.DesignTime.Migrations
{
    /// <inheritdoc />
    public partial class InitialObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var scripts = new[]
            {
                "vwFlight_Reservation_v1.sql",
                "vwCar_Reservation_v1.sql",
                "vwHotel_Reservation_v1.sql",
                "uspGetFlightReservationsByTripId_v1.sql",
                "uspGetCarReservationsByTripId_v1.sql",
                "uspGetHotelReservationsByTripId_v1.sql",
                "uspGetFlightReservationById_v1.sql",
                "uspGetFlightReservationsByUserId_v1.sql"
            };

            foreach (var file in scripts)
            {
                migrationBuilder.Sql(
                    ResourceLoader.ReadFileAsString(GetType().Assembly, file));
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
