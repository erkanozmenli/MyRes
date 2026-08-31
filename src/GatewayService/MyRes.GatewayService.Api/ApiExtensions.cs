using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyRes.BuildingBlocks.Authentication;
using MyRes.BuildingBlocks.Infrastructure.Extensions;
using MyRes.GatewayService.Api.Authentication;
using Scalar.AspNetCore;
using System.Net;
using System.Security.Claims;
using Yarp.ReverseProxy.Transforms;
using Microsoft.AspNetCore.HttpOverrides;
using MyRes.GatewayService.Api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyRes.GatewayService.Api
{
    public static class ApiExtensions
    {
        public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddOptions<Configuration.ApplicationUrlsOptions>()
                .BindConfiguration(Configuration.ApplicationUrlsOptions.SectionName);

            builder.Services
                .AddOptions<Configuration.ForwardedHeadersOptions>()
                .BindConfiguration(Configuration.ForwardedHeadersOptions.SectionName);

            builder.Services
                .AddOptions<Configuration.AuthenticationOptions>()
                .BindConfiguration(Configuration.AuthenticationOptions.SectionName);

            builder.Services
                .AddOptions<Microsoft.AspNetCore.Builder.ForwardedHeadersOptions>()
                .Configure<IOptions<Configuration.ForwardedHeadersOptions>>((options, configuration) =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;

                    // For Staging/Prod (K3S, K8S)
                    var knownNetwork = ParseNetwork(configuration.Value.KnownNetwork);
                    if (knownNetwork is not null)
                    {
                        options.KnownIPNetworks.Add(new System.Net.IPNetwork(knownNetwork.Value.Address, knownNetwork.Value.PrefixLength));
                    }

                    // For Local Development
                    var knownProxies = configuration.Value.KnownProxies;
                    if (knownProxies is not null)
                    {
                        foreach (var proxy in knownProxies)
                        {
                            if (!IPAddress.TryParse(proxy, out var address))
                            {
                                throw new InvalidOperationException($"Invalid ForwardedHeaders:KnownProxies value: '{proxy}'.");
                            }

                            options.KnownProxies.Add(address);
                        }
                    }
                });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = AuthenticationSchemes.Oidc;
            })
            .AddCookie()
            .AddOpenIdConnect(AuthenticationSchemes.Oidc, _ => { })
            .AddJwtBearer(AuthenticationSchemes.Bearer);

            // Cookie Options
            builder.Services
                .AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
                .Configure<RedisTicketStore>((options, ticketStore) =>
                {
                    options.SessionStore = ticketStore;
                });

            // Oidc Options
            builder.Services
                .AddOptions<OpenIdConnectOptions>(AuthenticationSchemes.Oidc)
                .Configure<IOptions<Configuration.AuthenticationOptions>>((options, authentication) =>
                {
                    var keycloak = authentication.Value.Keycloak;

                    options.Authority = keycloak.Authority;
                    options.ClientId = keycloak.ClientId;
                    options.ClientSecret = keycloak.ClientSecret;
                    options.ResponseType = "code";
                    options.SaveTokens = false;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.RequireHttpsMetadata = false;
                    options.SignedOutRedirectUri = "/";
                    options.TokenValidationParameters = new()
                    {
                        NameClaimType = "preferred_username"
                    };

                    options.PushedAuthorizationBehavior = PushedAuthorizationBehavior.Disable;

                    options.Events.OnTokenResponseReceived = context =>
                    {
                        var idToken = context.TokenEndpointResponse?.IdToken;

                        if (!string.IsNullOrWhiteSpace(idToken))
                        {
                            context.Properties!.StoreTokens(
                            [
                                new AuthenticationToken
                                {
                                    Name = "id_token",
                                    Value = idToken
                                }
                            ]);
                        }

                        return Task.CompletedTask;
                    };
                });

            // Jwt Bearer Options
            builder.Services
                .AddOptions<JwtBearerOptions>(AuthenticationSchemes.Bearer)
                .Configure<IOptions<Configuration.AuthenticationOptions>>((options, authentication) =>
                {
                    var keycloak = authentication.Value.Keycloak;

                    options.Authority = keycloak.Authority;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = keycloak.Audience
                    };
                });


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IRequestIdentity>(sp =>
            {
                var factory = sp.GetRequiredService<IClaimsPrincipalRequestIdentityFactory>();
                var accessor = sp.GetRequiredService<IHttpContextAccessor>();
                return factory.Create(accessor.HttpContext?.User ?? new ClaimsPrincipal());
            });

            builder.Services.AddSingleton<IClaimsPrincipalRequestIdentityFactory, ClaimsPrincipalRequestIdentityFactory>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("authenticated",
                    policy =>
                    {
                        policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme, AuthenticationSchemes.Bearer).RequireAuthenticatedUser();
                    });
            });


            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
                .AddTransforms(builderContext =>
                {
                    builderContext.AddRequestTransform(context =>
                    {
                        var identity = context.HttpContext.RequestServices.GetRequiredService<IRequestIdentity>();
                        var user = context.HttpContext.User;

                        if (!identity.IsAuthenticated)
                        {
                            return ValueTask.CompletedTask;
                        }

                        switch (identity.PrincipalType)
                        {
                            case PrincipalType.Anonymous:
                                break;
                            case PrincipalType.User:
                                AddHeader(context, GatewayHeaders.UserId, identity.UserId?.ToString());
                                AddHeader(context, GatewayHeaders.Username, identity.Username);
                                AddHeader(context, GatewayHeaders.Email, identity.Email);
                                break;
                            case PrincipalType.Client:
                                AddHeader(context, GatewayHeaders.ClientId, identity.ClientId);
                                break;
                            default:
                                break;
                        }

                        return ValueTask.CompletedTask;
                    });
                });

            builder.Services.AddOpenApi();

            builder.Services.AddOpenApi("MyRes", options =>
            {
                options.AddDocumentTransformer((doc, ctx, ct) =>
                {
                    doc.Info.Title = "MyRes API";
                    doc.Info.Description = "MyRes Endpoints";

                    return Task.CompletedTask;
                });
            });

            var logger = LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger("Startup");

            return builder;
        }

        public static WebApplication UseApi(this WebApplication app, IConfiguration configuration)
        {
            var serviceName = configuration["Service:Name"];
            var docs = configuration.GetSection(OpenApiDocumentOptions.SectionName).Get<List<OpenApiDocumentOptions>>() ?? new();

            app.UseApiRequestLogging();

            //app.Use(async (context, next) =>
            //{
            //    //app.Logger.LogInformation(
            //    //    "{Method} {Path}",
            //    //    context.Request.Method,
            //    //    context.Request.Path);

            //    var user = context.User;

            //    await next();
            //});

            app.Logger.LogInformation("Environment: {env}", app.Environment.EnvironmentName);

            app.MapGet("/health/ready", () =>
            {
                return Results.Ok($"{serviceName} readiness check OK");
            })
            .ExcludeFromDescription()
            .WithTags("Health");

            //app.MapGet("/cpu-burn", () =>
            //{
            //    var end = DateTime.UtcNow.AddSeconds(30);

            //    while (DateTime.UtcNow < end)
            //    {
            //        double x = 0;
            //        for (int i = 0; i < 1_000_000; i++)
            //            x += Math.Sqrt(i);
            //    }

            //    return Results.Ok("done");
            //})
            //.WithTags("CPU Burn");

            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/bff/auth-test", async context =>
            {
                if (context.User.Identity?.IsAuthenticated != true)
                {
                    await context.ChallengeAsync(AuthenticationSchemes.Oidc);
                    return;
                }

                await context.Response.WriteAsync($"Hello {context.User.Identity.Name}");
            });

            app.MapGet("/bff/login", async (HttpContext context, IOptions<ApplicationUrlsOptions> options, [FromQuery] string? returnUrl) =>
            {
                var origin = GetApplicationOrigin(context, options.Value);

                returnUrl = NormalizeReturnUrl(returnUrl);

                var redirectUri = $"{origin}{returnUrl}";

                if (context.User.Identity?.IsAuthenticated == true)
                {
                    context.Response.Redirect(redirectUri);
                    return;
                }
                app.Logger.LogInformation("Redirect uri: {uri}", redirectUri);
                await context.ChallengeAsync(AuthenticationSchemes.Oidc, new AuthenticationProperties { RedirectUri = redirectUri });
            });

            app.MapGet("/bff/logout", async (HttpContext context, IOptions<ApplicationUrlsOptions> options, [FromQuery] string? returnUrl) =>
            {
                var origin = GetApplicationOrigin(context, options.Value);

                returnUrl = NormalizeReturnUrl(returnUrl);

                var redirectUri = $"{origin}{returnUrl}";

                var authResult = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = authResult.Properties;

                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await context.SignOutAsync(AuthenticationSchemes.Oidc, properties);
            });

            app.MapGet("/bff/me", (IRequestIdentity identity) =>
            {
                if (!identity.IsAuthenticated)
                {
                    return Results.Ok(new { isAuthenticated = false });
                }

                return Results.Ok(new
                {
                    identity.IsAuthenticated,
                    identity.PrincipalType,
                    identity.UserId,
                    identity.Username,
                    identity.Email,
                    identity.ClientId
                });
            });

            app.MapOpenApi();

            app.MapScalarApiReference(options =>
            {
                foreach (var doc in docs)
                {
                    options.AddDocument(
                       doc.Name,
                       doc.Title,
                       doc.Route,
                       doc.IsDefault);
                }
            }).RequireAuthorization();

            app.MapGet("/", () => Results.Redirect("/scalar")).ExcludeFromDescription();

            //if (app.Environment.IsDevelopment())
            //{

            //}

            app.UseMiddleware<OpenApiUrlOverrideMiddleware>();

            app.MapReverseProxy();

            //app.UseHttpsRedirection();

            return app;
        }

        private static string NormalizeReturnUrl(string? returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
                return "/";

            if (!returnUrl.StartsWith('/'))
                return "/";

            if (returnUrl.StartsWith("//"))
                return "/";

            return returnUrl;
        }

        private static void AddHeader(RequestTransformContext context, string headerName, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            context.ProxyRequest.Headers.Remove(headerName);
            context.ProxyRequest.Headers.TryAddWithoutValidation(headerName, value);
        }

        private static (IPAddress Address, int PrefixLength)? ParseNetwork(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var parts = value.Split('/', 2);

            if (parts.Length != 2 || !IPAddress.TryParse(parts[0], out var address) || !int.TryParse(parts[1], out var prefixLength))
                throw new InvalidOperationException($"Invalid ForwardedHeaders:KnownNetwork value: '{value}'. " + "Expected format: '10.42.0.0/24'.");

            return (address, prefixLength);
        }

        static string GetApplicationOrigin(HttpContext context, ApplicationUrlsOptions options)
        {
            var origin = $"{context.Request.Scheme}://{context.Request.Host}";

            if (!options.AllowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Invalid application origin: {origin}");

            return origin;
        }
    }
}
