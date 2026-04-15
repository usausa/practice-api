var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Example_Web>("example")
    .WithHttpHealthCheck("/health");

builder.Build().Run();
