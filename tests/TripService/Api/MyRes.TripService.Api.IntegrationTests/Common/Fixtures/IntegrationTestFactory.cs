using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;

namespace MyRes.TripService.Api.IntegrationTests.Common.Fixtures
{
    public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private const string IntegrationTest = "IntegrationTest";

        private readonly TestEnvironmentOptions _options;
        private readonly MsSqlContainer? _container;

        private string? _connectionString;

        public HttpClient HttpClient { get; private set; } = default!;

        public IntegrationTestFactory()
        {
            _options = TestEnvironmentOptions.Load();

            if (!_options.UseLocalDb)
            {
                _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword(_options.Container.Password)
                .Build();
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(IntegrationTest);

            builder.ConfigureAppConfiguration((_, configBuilder) =>
            {
                if (!_options.UseLocalDb)
                {
                    var config = new Dictionary<string, string?>
                    {
                        [$"ConnectionStrings:TripServiceConnection"] = _connectionString
                    };

                    configBuilder.AddInMemoryCollection(config);
                }
                else
                {
                    Console.WriteLine("Using LOCAL DB from appsettings.IntegrationTest.json");
                }
            });
        }

        public async Task InitializeAsync()
        {
            if (!_options.UseLocalDb)
            {
                await _container!.StartAsync();
                var connectionString = _container.GetConnectionString();

                var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
                {
                    InitialCatalog = _options.Container.Database
                };

                _connectionString = connectionStringBuilder.ConnectionString;
            }
            else
            {
                Console.WriteLine("Using LOCAL DATABASE");
            }

            HttpClient = CreateClient();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            if (_container is not null)
                await _container.DisposeAsync();
        }
    }
}
