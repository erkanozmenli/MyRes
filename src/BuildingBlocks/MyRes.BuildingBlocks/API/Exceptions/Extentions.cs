using Microsoft.AspNetCore.Builder;

namespace MyRes.BuildingBlocks.Api.Exceptions
{
    public static class Extentions
    {
        public static IApplicationBuilder UseCustomStatusCodePages(this IApplicationBuilder app)
        {
            return app.UseMiddleware<StatusCodePagesMiddleware>();
        }
    }
}
