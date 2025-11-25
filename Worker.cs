using CatalogIngestionService.Providers;

namespace CatalogIngestionService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IContentProvider _contentProvider;

    public Worker(ILogger<Worker> logger, IContentProvider contentProvider)
    {
        _logger = logger;
        _contentProvider = contentProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        
            await FetchContent();
            await Task.Delay(60000, stoppingToken);
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
}
