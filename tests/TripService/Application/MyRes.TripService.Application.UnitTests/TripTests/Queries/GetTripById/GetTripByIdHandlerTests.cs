using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Abstractions.Metrics;
using MyRes.TripService.Application.Exceptions;
using MyRes.TripService.Application.Queries.Shared.Models;
using MyRes.TripService.Application.Trips.Queries.GetTripById;
using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;
using MyRes.TripService.Domain.Enums;


namespace MyRes.TripService.Application.UnitTests.TripTests.Queries.GetTripById
{
    public class GetTripByIdHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Trip_With_FlightReservations()
        {
            // Arrange
            var tripId = Guid.NewGuid();

            var tripDto = new TripDto
            {
                Id = tripId,
                TripNo = 123
            };

            var baseTime = DateTimeOffset.UtcNow;

            var flatRows = new List<FlightReservationFlatRow>
            {
                new(
                    tripId,
                    1001,
                    1,
                    10,
                    100,
                    null,
                    TripType.OneWay,
                    (int)FlightDirection.Outbound,
                    "IST",
                    "AMS",
                    baseTime,
                    baseTime.AddHours(2),
                    Guid.NewGuid()
                ),
                new(
                    tripId,
                    1001,
                    1,
                    10,
                    101,
                    100,
                    TripType.OneWay,
                    (int)FlightDirection.Outbound,
                    "AMS",
                    "CDG",
                    baseTime.AddHours(2),
                    baseTime.AddHours(5),
                    Guid.NewGuid()
                )
            };

            var tripQueryServiceMock = new Mock<ITripQueryService>();
            var flightQueryServiceMock = new Mock<IFlightReservationQueryService>();

            tripQueryServiceMock
                .Setup(x => x.GetTripByIdAsync(tripId))
                .ReturnsAsync(tripDto);

            flightQueryServiceMock
                .Setup(x => x.GetFlightReservationsByTripIdAsync(tripId))
                .ReturnsAsync(flatRows);

            var logger = NullLogger<GetTripByIdHandler>.Instance;
            var metricsMock = new Mock<ITripMetrics>();

            var handler = new GetTripByIdHandler(
                flightQueryServiceMock.Object,
                tripQueryServiceMock.Object,
                logger,
                metricsMock.Object
            );


            // Act
            var result = await handler.Handle(
                new GetTripByIdQuery(tripId),
                CancellationToken.None
            );


            // Assert
            result.Should().NotBeNull();
            result.Trip.Id.Should().Be(tripId);

            result.Trip.TripItems
                .OfType<FlightReservationDto>()
                .Should().NotBeEmpty();

            tripQueryServiceMock.Verify(x =>
                x.GetTripByIdAsync(tripId), Times.Once);

            flightQueryServiceMock.Verify(x =>
                x.GetFlightReservationsByTripIdAsync(tripId), Times.Once);
        }


        [Fact]
        public async Task Handle_Should_Throw_When_Trip_Not_Found()
        {
            // Arrange
            var tripId = Guid.NewGuid();

            var tripQueryServiceMock = new Mock<ITripQueryService>();
            var flightQueryServiceMock = new Mock<IFlightReservationQueryService>();

            tripQueryServiceMock
                .Setup(x => x.GetTripByIdAsync(tripId))
                .ReturnsAsync((TripDto?)null);

            var logger = NullLogger<GetTripByIdHandler>.Instance;
            var metricsMock = new Mock<ITripMetrics>();

            var handler = new GetTripByIdHandler(
                flightQueryServiceMock.Object,
                tripQueryServiceMock.Object,
                logger,
                metricsMock.Object
            );

            // Act
            Func<Task> act = async () =>
                await handler.Handle(new GetTripByIdQuery(tripId), CancellationToken.None);


            // Assert
            await act.Should()
                .ThrowAsync<TripNotFoundException>();
        }
    }
}
