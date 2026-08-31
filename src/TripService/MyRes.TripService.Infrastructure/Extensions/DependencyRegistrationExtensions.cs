using Microsoft.Extensions.DependencyInjection;
using MyRes.BuildingBlocks.Infrastructure.Data.Seed;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Abstractions.Metrics;
using MyRes.TripService.Infrastructure.Data.Persistence.Repositories;
using MyRes.TripService.Infrastructure.Data.QueryService;
using MyRes.TripService.Infrastructure.Data.QueryService.FlightReservation;
using MyRes.TripService.Infrastructure.Data.QueryService.Trip;
using MyRes.TripService.Infrastructure.Data.Seed;
using MyRes.TripService.Infrastructure.Telemetry;


namespace MyRes.TripService.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IDataSeeder, TripsDataSeeder>();
            services.AddScoped<IDapperExecuter, DapperExecutor>();
            services.AddScoped<ITripRepository, TripsRepository>();
            services.AddScoped<ITripQueryService, TripQueryService>();
            services.AddScoped<IFlightReservationQueryService, FlightReservationQueryService>();

            //Metrics
            services.AddSingleton<ITripMetrics, TripMetrics>();

            return services;
        }
    }
}
