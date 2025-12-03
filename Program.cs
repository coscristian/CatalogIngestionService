using CatalogIngestionService;
using CatalogIngestionService.Providers;
using CatalogIngestionService.WorkerClientServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSystemd();
builder.Services.AddHostedService<Worker>();
builder.Services.AddHttpClient<IContentProvider, TmdbProvider>((serviceProvider, client) =>
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var url = configuration["ApiClientConfig:TMDB:Url"];
        var version = configuration["ApiClientConfig:TMDB:Version"];
        client.BaseAddress = new Uri($"{url}/{version}/");
    });

builder.Services.AddHttpClient<IWorkerApiClient, WorkerApiClient>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var url = configuration["ApiClientConfig:WorkerApi:Url"];
    var version = configuration["ApiClientConfig:WorkerApi:Version"];
    client.BaseAddress = new Uri($"{url}/api/v{version}/");
});

var host = builder.Build();
host.Run();
