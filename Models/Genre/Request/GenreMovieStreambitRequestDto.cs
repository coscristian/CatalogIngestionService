using Newtonsoft.Json;

namespace CatalogIngestionService.Models.Genre.Request;

public class GenreMovieStreambitRequestDto
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
}