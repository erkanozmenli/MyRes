using FluentAssertions;
using MyRes.TripService.Api.Contracts.Enums;
using MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation;
using MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation.Contracts;
using MyRes.TripService.Api.IntegrationTests.Common.Base;
using MyRes.TripService.Api.IntegrationTests.Common.Fixtures;
using MyRes.TripService.Api.IntegrationTests.Helpers.Api;
using System.Net.Http.Json;

namespace MyRes.TripService.Api.IntegrationTests.EndpointTests.AddFlightReservation
{
    public class AddFlightReservationTests : IntegrationTestBase
    {
        private readonly TripApiHelper _tripApi;

        public AddFlightReservationTests(IntegrationTestFactory factory) : base(factory)
        {
            _tripApi = new TripApiHelper(Client);
        }

        [Fact]
        public async Task AddFlightReservation_Should_Return_201()
        {
            // Arrange
            var userId = Guid.NewGuid();

            SetIdentity(userId, "testUsername", "test@integrationtest.test");

            // Arrange
            var flightRequest = new AddFlightReservationRequest(
                    new FlightReservationInput(
                        TripType.OneWay,
                        new List<FlightInput>
                        {
                            new FlightInput(FlightDirection.Outbound ,
                            new List<FlightSegmentInput>
                            {
                                 new FlightSegmentInput(
                                     "IST",
                                     "JFK",
                                     DateTimeOffset.Parse("2026-06-10T08:00:00Z"),
                                     DateTimeOffset.Parse("2026-06-10T10:30:00Z")
                                     )
                            }
                        )}
                    )
                );

            // alternative json read
            //var flightRequest = ResourceLoader.ReadFileAsJson<AddFlightReservationRequest>(GetType().Assembly, "Valid_AddFlightReservation.json");

            var tripId = await _tripApi.CreateTripAsync();

            // Act
            var response = await Client.PostAsJsonAsync(
                    $"/v1/trips/{tripId}/flight-reservations",
                    flightRequest
            );


            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            var body = await response.Content.ReadFromJsonAsync<AddFlightReservationResponse>();

            body.Should().NotBeNull();
            body.TripId.Should().NotBeEmpty();
            body.FlightReservationId.Should().BeGreaterThan(0);
        }
    }
}
