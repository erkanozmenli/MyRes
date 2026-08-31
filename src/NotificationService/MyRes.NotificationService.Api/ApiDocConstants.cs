namespace MyRes.NotificationService.Api
{
    public static class ApiDocConstants
    {
        public const string Title = "Notification API";
        public const string Version = "v1";
        public const string Description = "Notification Service API Endpoints";

        public static class Versions
        {
            public static class V1
            {
                public const string Name = ApiVersions.V1;
                public const string DisplayName = "API V1";
                public const string JsonPath = "/openapi/v1.json";
            }
        }

        public static class ApiVersions
        {
            public const string V1 = "v1";
        }
    }
}
