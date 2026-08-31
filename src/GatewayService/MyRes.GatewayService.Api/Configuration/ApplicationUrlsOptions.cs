namespace MyRes.GatewayService.Api.Configuration
{
    public class ApplicationUrlsOptions
    {
        public const string SectionName = "ApplicationUrls";
        public List<string> AllowedOrigins { get; init; } = [];
    }
}
