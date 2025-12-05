using CatalogIngestionService.Models.Genre.Request;
using CatalogIngestionService.Models.Genre.Response;
using CatalogIngestionService.Models.Language.Request;
using CatalogIngestionService.Models.Language.Response;

namespace CatalogIngestionService.WorkerClientServices;

public interface IWorkerApiClient
{
    string ClientName { get; }
    Task<List<LanguageIngestStreambitResponseDto>> IngestLanguages(List<LanguageIngestStreambitRequest> languages);
    Task<List<GenreMovieStreambitResponseDto>> IngestGenres(List<GenreMovieStreambitRequestDto> genres);
}