using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyRes.BuildingBlocks.Testing.Utilities;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;
using Respawn;
using Testcontainers.MsSql;

namespace MyRes.TripService.Infrastructure.ContractTests.Common.Fixtures
{
    public class TripsDatabaseFixture : IAsyncLifetime
    {
        private readonly TestEnvironmentOptions _options;
        private readonly MsSqlContainer? _container;

        private string _connectionString = default!;
        private Respawner _respawner = default!;
        private SqlConnection _connection = default!;

        public string ConnectionString => _connectionString;


        public TripsDatabaseFixture()
        {
            _options = TestEnvironmentOptions.Load();

            if (!_options.UseLocalDb)
            {
                _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword(_options.Container.Password)
                .Build();
            }
        }

        public async Task InitializeAsync()
        {
            if (!_options.UseLocalDb)
                await _container!.StartAsync();

            _connectionString = ResolveConnectionString();

            TestDiagnostics.PrintContainerInfo("Reservations SQL Container", ConnectionString);

            await using var dbContext = CreateDbContext();

            // Migration
            await dbContext.Database.MigrateAsync();

            _connection = CreateConnection();

            await _connection.OpenAsync();

            _respawner = await Respawner.CreateAsync(
                _connection,
                new RespawnerOptions
                {
                    DbAdapter = DbAdapter.SqlServer,
                    TablesToIgnore =
                    [
                        "__EFMigrationsHistory"
                    ],
                    SchemasToInclude =
                    [
                        "dbo"
                    ]
                });
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_connection);
        }

        public async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
            if (!_options.UseLocalDb)
                await _container!.DisposeAsync();
        }

        public TripDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TripDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new TripDbContext(options);
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public string ResolveConnectionString()
        {
            if (_options.UseLocalDb)
                return _options.ConnectionStrings.TripServiceConnection;

            var connectionString = _container!.GetConnectionString();
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = _options.Container.Database
            };

            return builder.ConnectionString;
        }
    }
}
