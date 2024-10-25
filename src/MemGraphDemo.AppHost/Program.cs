var builder = DistributedApplication.CreateBuilder(args);

var memgraphUserFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "memgraph-user-files");
Console.WriteLine($"Binding volume mount for /etc/memgraph-user-files to: {memgraphUserFilesPath}");

var memgraph = builder.AddMemgraph("memgraph")
    .WithMemgraphLab()
    .WithVolume("memgraph-graph-data", "/var/lib/memgraph")
    .WithVolume("memgraph-user-data", "/usr/lib/memgraph")
    .WithVolume("memgraph-config", "/etc/memgraph")
    .WithVolume("memgraph-logs", "/var/log/memgraph")
    .WithBindMount(memgraphUserFilesPath, "/etc/memgraph-user-files", isReadOnly: false);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.MemGraphDemo_ApiService>("apiservice")
    .WithReference(memgraph);

builder.AddProject<Projects.MemGraphDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
