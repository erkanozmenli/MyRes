using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyRes.BuildingBlocks.Application.Behaviors;

namespace MyRes.TripService.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AssemblyInfo>();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<AssemblyInfo>();
                config.AddOpenBehavior(typeof(TelemetryBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
