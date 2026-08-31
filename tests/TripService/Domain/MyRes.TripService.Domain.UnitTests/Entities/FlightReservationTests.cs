using FluentAssertions;
using MyRes.TripService.Domain.Entities;
using MyRes.TripService.Domain.Enums;
using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.UnitTests.Entities
{
    public class FlightReservationTests
    {
        // =========================
        // ONE WAY VALIDATIONS
        // =========================

        [Fact]
        public void Create_OneWay_Should_Have_Exactly_One_Outbound()
        {
            // Arrange
            var flight = Flight.Create(
                FlightDirection.Outbound,
                new[] { CreateValidSegment() }
            );

            // Act
            var reservation = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight }
            );

            // Assert
            reservation.Flights.Should().HaveCount(1);
            reservation.Flights.Single().Direction.Should().Be(FlightDirection.Outbound);
        }


        [Fact]
        public void Create_OneWay_Should_Throw_When_No_Outbound()
        {
            // Arrange
            var flight = Flight.Create(
                FlightDirection.Inbound,
                new[] { CreateValidSegment() }
            );

            // Act
            Action act = () =>
                FlightReservation.Create(
                    TripType.OneWay,
                    new[] { flight }
                );

            // Assert
            act.Should()
               .Throw<OneWayTripMustHaveExactlyOneOutboundFlightException>();
        }


        [Fact]
        public void Create_OneWay_Should_Throw_When_Multiple_Outbound()
        {
            // Arrange
            var flights = new[]
            {
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() }),
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() })
            };

            // Act
            Action act = () =>
                FlightReservation.Create(
                    TripType.OneWay,
                    flights
                );

            // Assert
            act.Should()
               .Throw<OneWayTripMustHaveExactlyOneOutboundFlightException>();
        }


        [Fact]
        public void Create_OneWay_Should_Throw_When_Inbound_Exists()
        {
            // Arrange
            var flights = new[]
            {
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() }),
                Flight.Create(FlightDirection.Inbound, new[] { CreateValidSegment() })
            };

            // Act
            Action act = () =>
                FlightReservation.Create(
                    TripType.OneWay,
                    flights
                );

            // Assert
            act.Should()
               .Throw<OneWayTripMustHaveExactlyOneOutboundFlightException>();
        }



        // =========================
        // ROUND TRIP VALIDATIONS
        // =========================


        [Fact]
        public void Create_RoundTrip_Should_Have_One_Outbound_And_One_Inbound()
        {
            // Arrange
            var flights = new[]
            {
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() }),
                Flight.Create(FlightDirection.Inbound, new[] { CreateValidSegment() })
            };

            // Act
            var reservation = FlightReservation.Create(
                TripType.RoundTrip,
                flights
            );

            // Assert
            reservation.Flights.Should().HaveCount(2);
            reservation.Flights.Count(f => f.Direction == FlightDirection.Outbound).Should().Be(1);
            reservation.Flights.Count(f => f.Direction == FlightDirection.Inbound).Should().Be(1);
        }


        [Fact]
        public void Create_RoundTrip_Should_Throw_When_No_Inbound()
        {
            // Arrange
            var flights = new[]
            {
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() })
            };

            // Act
            Action act = () =>
                FlightReservation.Create(
                    TripType.RoundTrip,
                    flights
                );

            // Assert
            act.Should()
               .Throw<RoundTripRequiresInboundException>();
        }


        [Fact]
        public void Create_RoundTrip_Should_Throw_When_Multiple_Inbound()
        {
            // Arrange
            var flights = new[]
            {
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() }),
                Flight.Create(FlightDirection.Inbound, new[] { CreateValidSegment() }),
                Flight.Create(FlightDirection.Inbound, new[] { CreateValidSegment() })
            };

            // Act
            Action act = () =>
                FlightReservation.Create(
                    TripType.RoundTrip,
                    flights
                );

            // Assert
            act.Should()
               .Throw<RoundTripRequiresInboundException>();
        }


        [Fact]
        public void Create_RoundTrip_Should_Throw_When_Multiple_Outbound()
        {
            // Arrange
            var flights = new[]
            {
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() }),
                Flight.Create(FlightDirection.Outbound, new[] { CreateValidSegment() }),
                Flight.Create(FlightDirection.Inbound, new[] { CreateValidSegment() })
            };

            // Act
            Action act = () =>
                FlightReservation.Create(
                    TripType.RoundTrip,
                    flights
                );

            // Assert
            act.Should()
               .Throw<RoundTripRequiresInboundException>();
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
