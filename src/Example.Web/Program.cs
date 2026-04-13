using Example.Web.Endpoints;

using Microsoft.Extensions.Hosting.WindowsServices;

using Serilog;

// //--------------------------------------------------------------------------------
// Configure builder
//--------------------------------------------------------------------------------
Directory.SetCurrentDirectory(AppContext.BaseDirectory);
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
});

// Path
builder.Configuration.SetBasePath(AppContext.BaseDirectory);

// Service
builder.Host
    .UseWindowsService()
    .UseSystemd();

// Logging
builder.Logging.ClearProviders();
builder.Services.AddSerilog(options => options.ReadFrom.Configuration(builder.Configuration));

// TODO
builder.AddServiceDefaults();

// TODO
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// API
builder.Services.AddProblemDetails();

//--------------------------------------------------------------------------------
// Configure request pipeline.
//--------------------------------------------------------------------------------
var app = builder.Build();

// TODO
app.MapDefaultEndpoints();

if (!app.Environment.IsDevelopment())
{
    // TODO UseExceptionHandler() order ?
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// TODO API
app.MapDataEndpoints();

app.Run();
