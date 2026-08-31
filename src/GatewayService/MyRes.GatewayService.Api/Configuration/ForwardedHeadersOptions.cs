namespace MyRes.GatewayService.Api.Configuration
{
    public class ForwardedHeadersOptions
    {
        public const string SectionName = "ForwardedHeaders";
        public string KnownNetwork { get; init; } = string.Empty;
        public List<string> KnownProxies { get; init; } = [];
    }
}
