using Newtonsoft.Json;

namespace CatalogIngestionService.Models.Genre.Response;

public class GenreMovieItemResponseDto
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
}