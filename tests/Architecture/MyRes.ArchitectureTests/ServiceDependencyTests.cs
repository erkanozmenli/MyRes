using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace MyRes.ArchitectureTests
{
    public class ServiceDependencyTests
    {
        private static readonly IReadOnlyDictionary<string, Assembly[]> Services =
        new Dictionary<string, Assembly[]>
        {
            [nameof(TripService)] =
            [
                typeof(TripService.Api.AssemblyMarker).Assembly,
                typeof(TripService.Application.AssemblyMarker).Assembly,
                typeof(TripService.Domain.AssemblyMarker).Assembly,
                typeof(TripService.Infrastructure.AssemblyMarker).Assembly
            ],

            [nameof(PaymentService)] =
            [
                typeof(PaymentService.Api.AssemblyMarker).Assembly,
                typeof(PaymentService.Application.AssemblyMarker).Assembly,
                typeof(PaymentService.Domain.AssemblyMarker).Assembly,
                typeof(PaymentService.Infrastructure.AssemblyMarker).Assembly
            ],

            [nameof(ProviderService)] =
            [
                typeof(ProviderService.Api.AssemblyMarker).Assembly,
                typeof(ProviderService.Application.AssemblyMarker).Assembly,
                //typeof(ProviderService.Domain.AssemblyMarker).Assembly,
                typeof(ProviderService.Infrastructure.AssemblyMarker).Assembly
            ],

            [nameof(NotificationService)] =
            [
                typeof(NotificationService.Api.AssemblyMarker).Assembly,
                typeof(NotificationService.Application.AssemblyMarker).Assembly,
                typeof(NotificationService.Domain.AssemblyMarker).Assembly,
                typeof(NotificationService.Infrastructure.AssemblyMarker).Assembly
            ]
        };

        [Fact]
        public void Services_Should_Not_Depend_On_OtherServices()
        {
            var serviceAssemblies = Services.Values
               .SelectMany(x => x)
               .ToArray();

            foreach (var (serviceName, assemblies) in Services)
            {
                var otherServiceAssemblies = serviceAssemblies
                    .Except(assemblies)
                    .ToArray();

                foreach (var assembly in assemblies)
                {
                    foreach (var otherServiceAssembly in otherServiceAssemblies)
                    {
                        var result = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOn(otherServiceAssembly.GetName().Name!)
                            .GetResult();

                        result.IsSuccessful.Should().BeTrue(
                            $"{serviceName} ({assembly.GetName().Name}) " +
                            $"must not depend on {otherServiceAssembly.GetName().Name}");
                    }
                }
            }
        }
    }
}
