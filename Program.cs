using CatalogIngestionService;
using CatalogIngestionService.Providers;

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

var host = builder.Build();
host.Run();
