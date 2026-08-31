namespace MyRes.GatewayService.Api.Configuration
{
    public class AuthenticationOptions
    {
        public const string SectionName = "Authentication";
        public KeycloakOptions Keycloak { get; init; } = new();
    }

    public class KeycloakOptions
    {
        public string Authority { get; init; } = string.Empty;
        public string ClientId { get; init; } = string.Empty;
        public string ClientSecret { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
    }
}
