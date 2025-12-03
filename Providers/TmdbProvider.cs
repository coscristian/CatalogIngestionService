using CatalogIngestionService.Models;
using CatalogIngestionService.Models.Language;
using CatalogIngestionService.Models.Movie;

namespace CatalogIngestionService.Providers
{
    public class TmdbProvider : HttpApiClientBase, IContentProvider
    {
        private const string SecretKeyConfig = "ApiClientConfig:TMDB:SecretKey"; 
        public string Name { get; } = "Tmdb";
        
        public TmdbProvider(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration[SecretKeyConfig]!) { }


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
        
        public async Task<List<LanguageResponseDto>> FetchLanguagesAsync()
        {
            var response = await GetAsync<List<LanguageResponseDto>>("configuration/languages");
            return response;
        }
    }
}