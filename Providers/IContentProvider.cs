using CatalogIngestionService.Models;

namespace CatalogIngestionService.Providers
{
    public interface IContentProvider 
    {
        Task<DiscoverMoviesResponseDto> FetchMoviesAsync();  
    }
}