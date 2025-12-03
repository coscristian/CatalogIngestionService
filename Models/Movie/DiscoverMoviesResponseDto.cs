using Newtonsoft.Json;

namespace CatalogIngestionService.Models.Movie
{
    public class DiscoverMoviesResponseDto
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "results")]
        public DiscoverMovieItemDto Results  { get; set; } = new();

        [JsonProperty(PropertyName = "total_pages")]
        public string TotalPages { get; set; } = string.Empty;
        
        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }
    }
}