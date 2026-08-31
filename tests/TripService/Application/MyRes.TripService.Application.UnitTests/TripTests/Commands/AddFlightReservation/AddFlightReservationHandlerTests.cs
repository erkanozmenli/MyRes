using FluentAssertions;
using Moq;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;
using MyRes.TripService.Application.Trips.Commands.AddFlightReservation;
using MyRes.TripService.Application.Trips.Commands.AddFlightReservation.DTOs;
using MyRes.TripService.Domain.Entities.AggregateRoots;
using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.UnitTests.TripTests.Commands.AddFlightReservation
{
    public class AddFlightReservationHandlerTests
    {

        [Fact]
        public async Task Handle_Should_Add_FlightReservation_And_Save_And_Return_Result()
        {
            // Arrange
            var note = "Unit test trip";
            var userId = Guid.NewGuid();

            var trip = Trip.Create(note, userId);

            var repositoryMock = new Mock<ITripRepository>();

            repositoryMock
                .Setup(x => x.GetByGuidIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(trip);

            var handler = new AddFlightReservationHandler(repositoryMock.Object);

            var now = DateTimeOffset.UtcNow;

            var command = new AddFlightReservationCommand(
                trip.Id,
                new FlightReservationDto(
                    TripType.OneWay,
                    new[]
                    {
                        new FlightDto(
                            FlightDirection.Outbound,
                            new[]
                            {
                                new FlightSegmentDto(
                                    "IST",
                                    "AMS",
                                    now,
                                    now.AddHours(3)
                                )
                            }
                        )
                    }
                )
            );


            // Act
            var result = await handler.Handle(command, CancellationToken.None);


            // Assert

            // 1. Has reservation been added to Trip?
            trip.Lines
                .OfType<Domain.Entities.FlightReservation>()
                .Should().ContainSingle();

            var addedReservation = trip.Lines
                .OfType<Domain.Entities.FlightReservation>()
                .Single();

            // 2️. Is the result correct?
            result.TripId.Should().Be(trip.Id);
            result.FlightReservationId.Should().Be(addedReservation.Id);

            // 3️. Repository interaction
            repositoryMock.Verify(x => x.GetByGuidIdAsync(trip.Id), Times.Once);
            repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task Handle_Should_Throw_When_Trip_Not_Found()
        {
            // Arrange
            var repositoryMock = new Mock<ITripRepository>();

            var tripId = Guid.NewGuid();

            repositoryMock
                .Setup(x => x.GetByGuidIdAsync(tripId))
                .ReturnsAsync((Trip?)null);

            var handler = new AddFlightReservationHandler(repositoryMock.Object);

            var command = new AddFlightReservationCommand(
                    tripId,
                    new FlightReservationDto(
                        TripType.OneWay,
                        new List<FlightDto>())
                );


            // Act
            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);


            // Assert
            await act.Should()
                .ThrowAsync<TripNotFoundException>();
        }
    }
}
