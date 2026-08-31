namespace MyRes.GatewayService.Api.Configuration
{
    public class OpenApiDocumentOptions
    {
        public const string SectionName = "OpenApiDocuments";
        public string Name { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Route { get; set; } = default!;
        public bool IsDefault { get; set; }
        public bool UrlOverride { get; set; }
    }
}
