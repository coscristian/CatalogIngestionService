using CatalogIngestionService.Models.Genre.Request;
using CatalogIngestionService.Models.Genre.Response;
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

            //await FetchLanguagesAsync();
            await FetchGenresAsync();
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
    
    private async Task FetchGenresAsync()
    {
        try
        {
            _logger.LogInformation($"Fetching genres from {_contentProvider.Name}...");

            const string englishLanguage = "en";
            var genres = await _contentProvider.FetchGenresMovieAsync(englishLanguage);
            _logger.LogInformation($"Genres fecthed successfully from {_contentProvider.Name}");
            
            List<GenreMovieStreambitRequestDto> genresToIngest = TransformGenres(genres);
            
            _logger.LogInformation($"Ingesting genres into {_workerApiClient.ClientName}...");
            await _workerApiClient.IngestGenres(genresToIngest);
            _logger.LogInformation($"Genres ingested successfully into {_workerApiClient.ClientName}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error Fetching or ingesting genres: ", ex.Message);
        }
    }

    private List<GenreMovieStreambitRequestDto> TransformGenres(GenreMovieResponseDto genres)
    {
        return genres.Genres.Select(genreResponse => new GenreMovieStreambitRequestDto()
        {
            Id = genreResponse.Id,
            Name = genreResponse.Name
        }).ToList();
    }
}