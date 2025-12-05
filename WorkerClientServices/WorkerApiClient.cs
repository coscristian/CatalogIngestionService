using CatalogIngestionService.Models.Genre.Request;
using CatalogIngestionService.Models.Genre.Response;
using CatalogIngestionService.Models.Language.Request;
using CatalogIngestionService.Models.Language.Response;

namespace CatalogIngestionService.WorkerClientServices
{
    public class WorkerApiClient : HttpApiClientBase, IWorkerApiClient
    {
        public string ClientName { get; } = "Streambit";
        private const string SecretKeyConfig = ""; 
        
        public WorkerApiClient(HttpClient httpClient, IConfiguration configuration) : base(httpClient, /*configuration[SecretKeyConfig]!*/"")
        {
        }
        
        public async Task<List<LanguageIngestStreambitResponseDto>> IngestLanguages(List<LanguageIngestStreambitRequest> languages)
        {
            var response = await PostAsync<List<LanguageIngestStreambitRequest>, List<LanguageIngestStreambitResponseDto>>("Languages/CreateLanguages", languages);
            return response;
        }

        public async Task<List<GenreMovieStreambitResponseDto>> IngestGenres(List<GenreMovieStreambitRequestDto> genres)
        {
            var response = await PostAsync<List<GenreMovieStreambitRequestDto>, List<GenreMovieStreambitResponseDto>>("Genres/create", genres);
            return response;
        }
    }
}