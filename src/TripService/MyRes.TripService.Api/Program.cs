using MyRes.TripService.Infrastructure;
using MyRes.TripService.Api;
using MyRes.TripService.Application;

const string serviceName = "MyRes.TripService";
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

if (builder.Configuration.GetValue<bool>("Database:Initialize"))
    await app.UseTripServiceInitialization();

app.Run();

public partial class Program;