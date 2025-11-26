using CatalogIngestionService.Models;

namespace CatalogIngestionService.Providers
{
    public class TmdbProvider : HttpApiClientBase, IContentProvider
    {
        public TmdbProvider(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration["ApiClientConfig:TMDB:SecretKey"]!) { }

        public async Task<DiscoverMoviesResponseDto> FetchMoviesAsync()
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = "1",
                ["sort_by"] = "popularity.desc",
                ["language"] = "en-US"
            };

            var url = BuildUrlWithQueryParams("discover/movie", parameters);
            var response = await GetAsync<DiscoverMoviesResponseDto>(url);
            return response;
        } 
        
        
        
    }
}