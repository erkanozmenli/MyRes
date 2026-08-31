using FluentAssertions;
using MyRes.TripService.Domain.Entities;
using MyRes.TripService.Domain.Exceptions;

namespace MyRes.TripService.Domain.UnitTests.Entities
{
    public class FlightSegmentTests
    {
        [Fact]
        public void FlightSegment_Should_Throw_When_Departure_Is_After_Arrival()
        {
            // Arrange
            var departure = DateTimeOffset.UtcNow.AddHours(5);
            var arrival = DateTimeOffset.UtcNow.AddHours(2);

            // Act
            Action act = () =>
                FlightSegment.Create(
                    from: "IST",
                    to: "AMS",
                    departure: departure,
                    arrival: arrival
                );

            // Assert
            act.Should()
               .Throw<ArrivalTimeMustBeAfterDepartureTimeException>();
        }
    }
}
