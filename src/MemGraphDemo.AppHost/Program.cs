var builder = DistributedApplication.CreateBuilder(args);

var memgraph = builder.AddMemgraph("memgraph")
    .WithMemgraphLab()
    .WithVolume("memgraph-graph-data", "/var/lib/memgraph")
    .WithVolume("memgraph-user-data", "/usr/lib/memgraph")
    .WithVolume("memgraph-config", "/etc/memgraph")
    .WithVolume("memgraph-logs", "/var/log/memgraph");

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.MemGraphDemo_ApiService>("apiservice")
    .WithReference(memgraph);

builder.AddProject<Projects.MemGraphDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
