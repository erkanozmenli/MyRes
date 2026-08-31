using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRes.BuildingBlocks.Infrastructure.Data.Interceptors;
using MyRes.PaymentService.Infrastructure.Data.Persistence.Contexts;
using System.Data;


namespace MyRes.PaymentService.Infrastructure.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            // EF
            services.AddDbContext<PaymentDbContext>((sp, options) =>
            {
                var connectionString = configuration.GetConnectionString(Constants.PaymentServiceConnection);

                options.AddInterceptors(
                    sp.GetServices<ISaveChangesInterceptor>());

                options.UseSqlServer(connectionString, sql =>
                {
                    sql.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                });
            });

            // Dapper
            services.AddScoped<IDbConnection>(_ =>
            {
                var connectionString = configuration.GetConnectionString(Constants.PaymentServiceConnection);

                return new SqlConnection(connectionString);
            });

            return services;
        }
    }
}
