using MyRes.GatewayService.Api;

const string serviceName = "MyRes.GatewayService";
const string serviceVersion = "1.0.0";
//------------------------------------------

var builder = WebApplication.CreateBuilder(args);

builder.Configuration["Service:Name"] = serviceName;
builder.Configuration["Service:Version"] = serviceVersion;

builder.AddApi();

builder.AddInfrastructure();

var app = builder.Build();

app.UseApi(builder.Configuration);

app.Run();

public partial class Program;