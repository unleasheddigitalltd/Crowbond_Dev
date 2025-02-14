using Crowbond.Api;
using Crowbond.Api.Extensions;
using Crowbond.Common.Presentation.Endpoints;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

// Add environment variable replacement first
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbConnString = builder.Configuration.GetConnectionString("Database")
    ?.Replace("${DB_PASSWORD}", dbPassword);
builder.Configuration["ConnectionStrings:Database"] = dbConnString;

builder.Configuration.AddModuleConfiguration(["users", "wms", "crm", "oms"]);

// Now add services with the updated connection string
builder.Services.AddServices(builder.Configuration);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.ApplyMigrations();
app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAntiforgery();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

app.Run();

public partial class Program;
