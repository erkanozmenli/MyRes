using MyRes.BuildingBlocks.Authentication;
using MyRes.TripService.Api.IntegrationTests.Common.Collections;
using MyRes.TripService.Api.IntegrationTests.Common.Fixtures;

namespace MyRes.TripService.Api.IntegrationTests.Common.Base
{
    [Collection(nameof(IntegrationTestCollection))]
    public abstract class IntegrationTestBase
    {
        protected readonly HttpClient Client;

        protected IntegrationTestBase(IntegrationTestFactory factory)
        {
            Client = factory.HttpClient;
        }

        protected void SetIdentity(Guid userId, string username = "integration-test-user", string email = "integration-test@test.com")
        {
            Client.DefaultRequestHeaders.Remove(GatewayHeaders.UserId);
            Client.DefaultRequestHeaders.Remove(GatewayHeaders.Username);
            Client.DefaultRequestHeaders.Remove(GatewayHeaders.Email);

            Client.DefaultRequestHeaders.Add(GatewayHeaders.UserId, userId.ToString());
            Client.DefaultRequestHeaders.Add(GatewayHeaders.Username, username);
            Client.DefaultRequestHeaders.Add(GatewayHeaders.Email, email);
        }
    }
}
