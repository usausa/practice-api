var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Example_Web>("example");

builder.Build().Run();
