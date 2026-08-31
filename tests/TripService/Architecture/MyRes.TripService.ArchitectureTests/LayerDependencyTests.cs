using System.Reflection;
using NetArchTest.Rules;


namespace MyRes.TripService.ArchitectureTests
{
    public class LayerDependencyTests
    {
        private static readonly Assembly DomainAssembly = typeof(MyRes.TripService.Domain.AssemblyMarker).Assembly;
        private static readonly Assembly ApplicationAssembly = typeof(MyRes.TripService.Application.AssemblyMarker).Assembly;
        private static readonly Assembly InfrastructureAssembly = typeof(MyRes.TripService.Infrastructure.AssemblyMarker).Assembly;
        private static readonly Assembly ApiAssembly = typeof(MyRes.TripService.Api.AssemblyMarker).Assembly;

        [Fact]
        public void Domain_Should_Not_Depend_On_Application()
        {
            var result = Types
                .InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOn(ApplicationAssembly.GetName().Name!)
                .GetResult();

            ArchitectureTestAssertions.AssertArchitectureRule(result, "Domain should not depend on Application");
        }

        [Fact]
        public void Domain_Should_Not_Depend_On_Infrastructure()
        {
            var result = Types
                .InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOn(InfrastructureAssembly.GetName().Name!)
                .GetResult();

            ArchitectureTestAssertions.AssertArchitectureRule(result, "Domain should not depend on Infrastructure");
        }

        [Fact]
        public void Domain_Should_Not_Depend_On_Api()
        {
            var result = Types
                .InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOn(ApiAssembly.GetName().Name!)
                .GetResult();

            ArchitectureTestAssertions.AssertArchitectureRule(result, "Domain should not depend on Api");
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure()
        {
            var result = Types
                .InAssembly(ApplicationAssembly)
                .ShouldNot()
                .HaveDependencyOn(InfrastructureAssembly.GetName().Name!)
                .GetResult();

            ArchitectureTestAssertions.AssertArchitectureRule(result, "Application should not depend on Infrastructure");
        }

        [Fact]
        public void Infrastructure_Should_Not_Depend_On_Api()
        {
            var result = Types
                .InAssembly(InfrastructureAssembly)
                .ShouldNot()
                .HaveDependencyOn(ApiAssembly.GetName().Name!)
                .GetResult();

            ArchitectureTestAssertions.AssertArchitectureRule(result, "Infrastructure should not depend on Api");
        }

        [Fact]
        public void Api_Should_Not_Depend_On_Domain()
        {
            var result = Types
                .InAssembly(ApiAssembly)
                .ShouldNot()
                .HaveDependencyOn(DomainAssembly.GetName().Name!)
                .GetResult();

            ArchitectureTestAssertions.AssertArchitectureRule(result, "API should not depend on Domain");
        }
    }
}
