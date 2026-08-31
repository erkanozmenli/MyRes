using MyRes.TripService.Api.Endpoints.Trips.CreateTrip;
using MyRes.TripService.Api.Endpoints.Trips.CreateTrip.Contracts;
using System.Net.Http.Json;

namespace MyRes.TripService.Api.IntegrationTests.Helpers.Api
{
    public class TripApiHelper
    {
        private readonly HttpClient _client;

        public TripApiHelper(HttpClient client)
        {
            _client = client;
        }

        public async Task<Guid> CreateTripAsync(CreateTripRequest? request = null)
        {
            request ??= new CreateTripRequest(new TripInput("Integration Test Trip"));

            var response = await _client.PostAsJsonAsync("/v1/trips", request);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadFromJsonAsync<CreateTripResponse>();

            return body!.Id;
        }
    }
}
