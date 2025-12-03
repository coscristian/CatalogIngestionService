using CatalogIngestionService.Models.Language;
using CatalogIngestionService.Models.Language.Request;
using CatalogIngestionService.Providers;
using CatalogIngestionService.WorkerClientServices;

namespace CatalogIngestionService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IContentProvider _contentProvider;
    private readonly IWorkerApiClient _workerApiClient;

    public Worker(ILogger<Worker> logger, IContentProvider contentProvider, IWorkerApiClient workerApiClient)
    {
        _logger = logger;
        _contentProvider = contentProvider;
        _workerApiClient = workerApiClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await FetchLanguagesAsync();
            await Task.Delay(120000, stoppingToken);
        }
    }

    private async Task FetchContent()
    {
        try
        {
            var movies = await _contentProvider.FetchMoviesAsync();
            _logger.LogInformation("[SUCCESS]: Getting movies: ",  movies.ToString());
        }catch(Exception ex)
        {
            _logger.LogError("[ERROR]: Getting movies: ", ex.Message);
        }        
    }

    private async Task FetchLanguagesAsync()
    {
        try
        {
            _logger.LogInformation($"Fetching languages from {_contentProvider.Name}...");
            var languages = await _contentProvider.FetchLanguagesAsync();
            _logger.LogInformation($"Languages fecthed successfully from {_contentProvider.Name}");
            
            var languagesToIngest = TransformLanguages(languages);
            
            _logger.LogInformation($"Ingesting languages into {_workerApiClient.ClientName}...");
            await _workerApiClient.IngestLanguages(languagesToIngest);
            _logger.LogInformation($"Languages ingested successfully into {_workerApiClient.ClientName}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error Fetching or ingesting languages: ", ex.Message);
        }
    }

    private static List<LanguageIngestStreambitRequest> TransformLanguages(List<LanguageResponseDto> languages)
    {
        return languages
            .Select(l => new LanguageIngestStreambitRequest
            {
                Name = l.Name,
                EnglishName = l.EnglishName,
                Iso6391 = l.Iso6391
            })
            .ToList();
    }
}