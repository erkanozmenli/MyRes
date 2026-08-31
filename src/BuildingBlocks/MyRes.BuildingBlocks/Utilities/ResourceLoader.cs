using System.Reflection;
using System.Text.Json;

namespace MyRes.BuildingBlocks.Utilities
{
    public static class ResourceLoader
    {
        public static string ReadFileAsString(Assembly assembly, string fileName)
        {
            var resource = assembly.GetManifestResourceNames()
                .FirstOrDefault(x => x.EndsWith($".{fileName}", StringComparison.OrdinalIgnoreCase));

            if (resource == null)
                throw new InvalidOperationException($"File not found: {fileName}");

            using var stream = assembly.GetManifestResourceStream(resource)!;
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public static T ReadFileAsJson<T>(Assembly assembly, string fileName)
        {
            var resource = assembly.GetManifestResourceNames()
                .SingleOrDefault(x => x.EndsWith($".{fileName}", StringComparison.OrdinalIgnoreCase));

            if (resource == null)
                throw new InvalidOperationException($"File not found: {fileName}");

            using var stream = assembly.GetManifestResourceStream(resource)!;
            using var reader = new StreamReader(stream);

            var json = reader.ReadToEnd();

            return JsonSerializer.Deserialize<T>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                )!;
        }
    }
}
