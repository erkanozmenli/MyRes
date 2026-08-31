using Microsoft.EntityFrameworkCore.Migrations;
using MyRes.BuildingBlocks.Utilities;

#nullable disable

namespace MyRes.TripService.Infrastructure.Data.DesignTime.Migrations
{
    /// <inheritdoc />
    public partial class uspGetFlightReservationById_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var scripts = new[]
            {
                "uspGetFlightReservationById_v2.sql"
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
