using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MyRes.BuildingBlocks.Api.OpenApi
{
    public static class Extentions
    {
        public static TBuilder WithDoc<TBuilder>(this TBuilder builder, EndpointDocumentation docs) where TBuilder : IEndpointConventionBuilder
        {
            builder.WithSummary(docs.Summary);
            builder.WithDescription(docs.Description);

            return builder;
        }

    }
}
