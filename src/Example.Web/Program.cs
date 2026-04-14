using Example.Web.Application;

using Microsoft.Extensions.Hosting.WindowsServices;

// //--------------------------------------------------------------------------------
// Configure builder
//--------------------------------------------------------------------------------
Directory.SetCurrentDirectory(AppContext.BaseDirectory);
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
});

// System
builder.ConfigureSystem();

// Host
builder.ConfigureHost();

// Logging
builder.ConfigureLogging();

// Http
builder.ConfigureHttp();
// API
builder.ConfigureApi();
// Compress
//builder.ConfigureCompression();
// TODO
//// Swagger
//builder.ConfigureSwagger();

// Health
builder.ConfigureHealth();
// TODO
// Metrics
//builder.ConfigureTelemetry();

// TODO
// Components
//builder.ConfigureComponents();

//--------------------------------------------------------------------------------
// Configure request pipeline.
//--------------------------------------------------------------------------------
var app = builder.Build();

// Startup information
app.LogStartupInformation();

// TODO
// Logging
//app.UseLogging();
//app.UseLoggingContext();

// TODO order

// TODO
//// Forwarded headers
//app.UseForwardedHeaders();

// TODO
//// Buffered response
//app.UseBufferedResponse();

// Error handler
app.UseErrorHandler();

// TODO
// Compression
//app.UseCompression();

// End point
app.MapEndpoints();

// Initialize
await app.InitializeApplicationAsync();

// Run
await app.RunAsync();

public partial class Program
{
}
