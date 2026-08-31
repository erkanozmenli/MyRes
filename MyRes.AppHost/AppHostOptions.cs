namespace MyRes.AppHost
{
    public class AppHostOptions
    {
        public bool UseLocalDb { get; set; }
        public bool UseExternalFrontend { get; set; }
        public ConnectionStringsOptions ConnectionStrings { get; set; } = new();
        public ParametersOptions Parameters { get; set; } = new();
        public OpenTelemetryOptions OpenTelemetry { get; set; } = new();
    }

    public class ConnectionStringsOptions
    {
        public string DefaultConnection { get; set; } = default!;
    }

    public class ParametersOptions
    {
        public string SqlPassword { get; set; } = default!;
    }

    public class OpenTelemetryOptions
    {
        public string CollectorEndpoint { get; init; } = string.Empty;
        public bool UseAspire { get; set; }
    }
}
