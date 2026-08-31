using FluentAssertions;
using MyRes.TripService.Domain.Entities;
using MyRes.TripService.Domain.Entities.AggregateRoots;
using MyRes.TripService.Domain.Enums;
using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.UnitTests.Entities.AggregateRoots
{
    public class TripTests
    {
        [Fact]
        public void Create_Should_Initialize_Empty_Lines()
        {
            // Arrange
            var note = "Unit test trip";
            var userId = Guid.NewGuid();

            // Act
            var trip = Trip.Create(note, userId);

            // Assert
            trip.Lines.Should().NotBeNull();
            trip.Lines.Should().BeEmpty();
        }

        [Fact]
        public void AddFlightReservation_Should_Add_FlightReservation_To_Lines()
        {
            // Arrange
            var note = "Unit test trip";
            var userId = Guid.NewGuid();

            var trip = Trip.Create(note, userId);

            var segment = FlightSegment.Create(
                "IST",
                "AMS",
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow.AddHours(3)
            );

            var flight = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment }
            );

            var flightReservation = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight }
            );

            // Act
            trip.AddFlightReservation(flightReservation);

            // Assert
            trip.Lines.OfType<FlightReservation>().Should().HaveCount(1);
        }

        [Fact]
        public void AddFlightReservation_Should_Throw_When_Segments_Overlap()
        {
            // Arrange
            var note = "Unit test trip";
            var userId = Guid.NewGuid();

            var trip = Trip.Create(note, userId);

            var departure = DateTimeOffset.UtcNow;

            // First reservation (00:00 - 03:00)
            var segment1 = FlightSegment.Create(
                "IST",
                "AMS",
                departure,
                departure.AddHours(3)
            );

            var flight1 = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment1 }
            );

            var reservation1 = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight1 }
            );

            trip.AddFlightReservation(reservation1);


            // Second reservation overlaps (02:00 - 05:00)
            var segment2 = FlightSegment.Create(
                "AMS",
                "CDG",
                departure.AddHours(2),   // overlap starts here
                departure.AddHours(5)
            );

            var flight2 = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment2 }
            );

            var reservation2 = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight2 }
            );

            // Act
            Action act = () => trip.AddFlightReservation(reservation2);

            // Assert
            act.Should().Throw<FlightSegmentDatesOverlapException>();
        }

        [Fact]
        public void AddFlightReservation_Should_Not_Throw_When_Segments_Do_Not_Overlap()
        {
            // Arrange
            var note = "Unit test trip";
            var userId = Guid.NewGuid();

            var trip = Trip.Create(note, userId);

            var departure = DateTimeOffset.UtcNow;

            // First reservation
            var segment1 = FlightSegment.Create(
                "IST",
                "AMS",
                departure,
                departure.AddHours(3)
            );

            var flight1 = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment1 }
            );

            var reservation1 = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight1 }
            );

            trip.AddFlightReservation(reservation1);


            // Second reservation overlaps
            var segment2 = FlightSegment.Create(
                "AMS",
                "CDG",
                departure.AddHours(3),
                departure.AddHours(5)
            );

            var flight2 = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment2 }
            );

            var reservation2 = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight2 }
            );

            // Act
            Action act = () => trip.AddFlightReservation(reservation2);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ChangeFlightSegmentDates_Should_Update_Segment_Dates_When_Valid()
        {
            // Arrange
            var note = "Unit test trip";
            var userId = Guid.NewGuid();

            var trip = Trip.Create(note, userId);

            var departure = DateTimeOffset.UtcNow;

            var segment = FlightSegment.Create(
                "IST",
                "AMS",
                departure,
                departure.AddHours(3)
            );

            var flight = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment }
            );

            var reservation = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight }
            );

            trip.AddFlightReservation(reservation);

            var newDeparture = departure.AddHours(1);
            var newArrival = departure.AddHours(4);


            // Act
            trip.ChangeFlightSegmentDates(
                flightReservationId: reservation.Id,
                flightId: flight.Id,
                flightSegmentId: segment.Id,
                departureTime: newDeparture,
                arrivalTime: newArrival
            );


            // Assert
            var updatedSegment = trip.Lines
                .OfType<FlightReservation>()
                .Single()
                .Flights.Single()
                .Segments.Single();

            updatedSegment.DepartureTime.Should().Be(newDeparture);
            updatedSegment.ArrivalTime.Should().Be(newArrival);
        }

        [Fact]
        public void ChangeFlightSegmentDates_Should_Throw_When_FlightReservation_Not_Found()
        {
            // Arrange
            var note = "Unit test trip";
            var userId = Guid.NewGuid();

            var trip = Trip.Create(note, userId);

            var segment = FlightSegment.Create(
                "IST",
                "AMS",
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow.AddHours(3)
            );

            var flight = Flight.Create(
                FlightDirection.Outbound,
                new[] { segment }
            );

            var reservation = FlightReservation.Create(
                TripType.OneWay,
                new[] { flight }
            );

            trip.AddFlightReservation(reservation);


            // Act
            Action act = () => trip.ChangeFlightSegmentDates(
                flightReservationId: int.MaxValue, // non-existing id
                flightId: flight.Id,
                flightSegmentId: segment.Id,
                departureTime: DateTimeOffset.UtcNow.AddHours(1),
                arrivalTime: DateTimeOffset.UtcNow.AddHours(4)
            );


            // Assert
            act.Should()
               .Throw<FlightReservationDoesNotExistException>();
        }
    }
}
