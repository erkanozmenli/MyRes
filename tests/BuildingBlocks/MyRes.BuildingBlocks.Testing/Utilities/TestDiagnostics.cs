using System.Diagnostics;

namespace MyRes.BuildingBlocks.Testing.Utilities
{
    public static class TestDiagnostics
    {
        public static void PrintContainerInfo(string name, string connectionString)
        {
            var message = ($"""
            ==========================================
            {name}
            {connectionString}
            ==========================================
            """);

            Console.WriteLine(message);
            Debug.WriteLine(message);
        }
    }
}
