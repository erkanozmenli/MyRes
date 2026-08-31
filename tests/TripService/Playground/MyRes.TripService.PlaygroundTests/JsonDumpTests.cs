using AutoFixture;
using MyRes.TripService.Api.Endpoints.Trips.FlightReservations.AddFlightReservation;
using MyRes.TripService.Application.Trips.Commands.CreateTrip.DTOs;
using System.Text.Json;
using Xunit.Abstractions;

namespace MyRes.TripService.PlaygroundTests
{
    public class JsonDumpTests
    {
        private readonly ITestOutputHelper _output;
        private readonly Fixture _fixture;

        public JsonDumpTests(ITestOutputHelper output)
        {
            _output = output;
            _fixture = new Fixture();
        }


        [Fact(Skip = "Playground only")]
        public void Dump_ReservationDto()
        {
            var obj = _fixture.Create<TripDto>();

            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
            });

            _output.WriteLine(json);
        }


        [Fact(Skip = "Playground only")]
        public void Dump_AddFlightReservationRequest()
        {
            var obj = _fixture.Create<AddFlightReservationRequest>();

            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
            });

            _output.WriteLine(json);
        }

    }
}
