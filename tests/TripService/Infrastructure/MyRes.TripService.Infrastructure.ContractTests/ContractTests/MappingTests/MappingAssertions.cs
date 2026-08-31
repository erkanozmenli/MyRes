namespace MyRes.TripService.Infrastructure.ContractTests.ContractTests.MappingTests
{
    public static class MappingAssertions
    {
        public static void AssertMapping<T>(IDictionary<string, object> row)
        {
            var modelProps = typeof(T).GetProperties()
                .Select(x => x.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var dbColumns = row.Keys
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var prop in modelProps)
            {
                Assert.True(dbColumns.Contains(prop), $"Missing column in DB: {prop}");
            }

            foreach (var column in dbColumns)
            {
                Assert.True(modelProps.Contains(column), $"Extra column in DB not mapped: {column}");
            }
        }
    }
}
