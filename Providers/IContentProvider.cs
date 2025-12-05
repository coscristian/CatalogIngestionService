using CatalogIngestionService.Models;
using CatalogIngestionService.Models.Genre.Response;
using CatalogIngestionService.Models.Language;
using CatalogIngestionService.Models.Movie;

namespace CatalogIngestionService.Providers
{
    public interface IContentProvider 
    {
        string Name { get; }
        Task<DiscoverMoviesResponseDto> FetchMoviesAsync();
        Task<List<LanguageResponseDto>> FetchLanguagesAsync();
        Task<GenreMovieResponseDto> FetchGenresMovieAsync(string language);
    }
}