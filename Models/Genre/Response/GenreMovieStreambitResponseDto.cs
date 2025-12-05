using Newtonsoft.Json;

namespace CatalogIngestionService.Models.Genre.Response;

public class GenreMovieStreambitResponseDto
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }
    
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonProperty(PropertyName = "lastModified")]
    public DateTime LastModified { get; set; }
}