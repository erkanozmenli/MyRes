using FluentAssertions;
using MyRes.TripService.Domain.Entities;
using MyRes.TripService.Domain.Enums;
using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.UnitTests.Entities
{
    public class FlightTests
    {
        [Fact]
        public void Create_Should_Set_Direction()
        {
            // Arrange
            var segment = CreateValidSegment();

            // Act
            var flight = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment }
            );

            // Assert
            flight.Direction.Should().Be(FlightDirection.Outbound);
        }


        [Fact]
        public void Create_Should_Add_Segments()
        {
            // Arrange
            var segment1 = CreateValidSegment();
            var segment2 = CreateValidSegment();

            // Act
            var flight = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment1, segment2 }
            );

            // Assert
            flight.Segments.Should().HaveCount(2);
            flight.Segments.Should().Contain(segment1);
            flight.Segments.Should().Contain(segment2);
        }


        [Fact]
        public void Create_Should_Throw_When_No_Segments()
        {
            // Act
            Action act = () =>
                Flight.Create(
                    FlightDirection.Outbound,
                    new FlightSegment[] { }
                );

            // Assert
            act.Should()
               .Throw<FlightMustHaveAtLeastOneSegmentException>();
        }


        // =========================
        // Helper
        // =========================

        private static FlightSegment CreateValidSegment()
        {
            var now = DateTimeOffset.UtcNow;

            return FlightSegment.Create(
                from: "IST",
                to: "AMS",
                departure: now,
                arrival: now.AddHours(3)
            );
        }

    }
}
