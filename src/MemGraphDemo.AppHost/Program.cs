var builder = DistributedApplication.CreateBuilder(args);

var memgraph = builder.AddMemgraph("memgraph")
    .WithMemgraphLab();

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.MemGraphDemo_ApiService>("apiservice")
    .WithReference(memgraph);

builder.AddProject<Projects.MemGraphDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
