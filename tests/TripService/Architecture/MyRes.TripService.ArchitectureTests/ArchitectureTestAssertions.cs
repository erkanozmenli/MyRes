using FluentAssertions;
using NetArchTest.Rules;


namespace MyRes.TripService.ArchitectureTests
{
    public static class ArchitectureTestAssertions
    {
        public static void AssertArchitectureRule(TestResult result, string rule)
        {
            if (result.IsSuccessful)
                return;

            result.IsSuccessful.Should().BeTrue(
                $"{rule}. " + $"Failing types: " +
                $"{Environment.NewLine}" + string.Join(Environment.NewLine, result.FailingTypes.Select(x => $" - {x.FullName}")));
        }
    }
}
