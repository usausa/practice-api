var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Example>("example");

builder.Build().Run();
