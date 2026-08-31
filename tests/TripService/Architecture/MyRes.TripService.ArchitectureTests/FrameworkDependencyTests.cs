using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using System.Reflection;

namespace MyRes.TripService.ArchitectureTests
{
    public class FrameworkDependencyTests
    {
        private static readonly Assembly DomainAssembly = typeof(MyRes.TripService.Domain.AssemblyMarker).Assembly;

        [Fact]
        public void Domain_Should_Not_Depend_On_EntityFramework()
        {
            var entityFrameworkAssembly = typeof(DbContext).Assembly;

            var result = Types
                .InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOn(entityFrameworkAssembly.GetName().Name!)
                .GetResult();

            ArchitectureTestAssertions.AssertArchitectureRule(result, "Domain should not depend on EntityFramework");
        }

    }
}
