using Microsoft.Extensions.Configuration;

namespace MyRes.TripService.Api.IntegrationTests
{
    public class TestEnvironmentOptions
    {
        public bool UseLocalDb { get; set; }
        public ConnectionStringOptions ConnectionStrings { get; set; } = new();
        public ContainerOptions Container { get; set; } = new();

        public static TestEnvironmentOptions Load()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var options = new TestEnvironmentOptions();
            config.Bind(options);

            return options;
        }
    }

    public class ConnectionStringOptions
    {
        public string TripServiceConnection { get; set; } = default!;
    }

    public class ContainerOptions
    {
        public string Password { get; set; } = default!;
        public string Database { get; set; } = default!;
    }
}
