using Newtonsoft.Json;

namespace CatalogIngestionService.Models.Genre.Response;

public class GenreMovieResponseDto
{
    [JsonProperty(PropertyName = "genres")]
    public List<GenreMovieItemResponseDto> Genres { get; set; } = [];
}