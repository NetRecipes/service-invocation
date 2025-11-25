var builder = DistributedApplication.CreateBuilder(args);

var servicea = builder
    .AddProject<Projects.ServiceA>("servicea")
    .WithDaprSidecar();

var serviceb = builder
    .AddProject<Projects.ServiceB>("serviceb")
    .WithDaprSidecar();

builder.Build().Run();
