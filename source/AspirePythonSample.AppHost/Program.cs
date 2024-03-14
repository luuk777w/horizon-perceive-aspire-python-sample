var builder = DistributedApplication.CreateBuilder(args);

var cannyEdgeToolContainer = builder
    .AddContainer("Canny-Edge-Detection-Tool", "canny-edge-tool")
    .WithEndpoint(hostPort: 50051, containerPort: 50051, name: "canny-edge-tool-endpoint");

var cannyEdgeToolEndpoint = cannyEdgeToolContainer
    .GetEndpoint("canny-edge-tool-endpoint");

builder.AddProject<Projects.AspirePythonSample_CannyEdgeService_API>("cannyedgeservice");

builder.Build().Run();
