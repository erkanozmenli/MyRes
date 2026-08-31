using MyRes.NotificationService.Api;
using MyRes.NotificationService.Application;
using MyRes.NotificationService.Infrastructure;

const string serviceName = "MyRes.NotificationService";
const string serviceVersion = "1.0.0";
//------------------------------------------

var builder = WebApplication.CreateBuilder(args);

builder.Configuration["Service:Name"] = serviceName;
builder.Configuration["Service:Version"] = serviceVersion;

builder.AddApi();
builder.Services.AddApplication();
builder.AddInfrastructure();

var app = builder.Build();

app.UseApi();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("DevCluster"))
{
    await app.UseNotificationServiceInitialization();
}

app.Run();

public partial class Program;