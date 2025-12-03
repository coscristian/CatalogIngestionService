using Newtonsoft.Json;

namespace CatalogIngestionService.Models.Language;

public class LanguageItemResponseDto
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "english_name")]
    public string EnglishName { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "iso_639_1")]
    public string Iso6391 { get; set; } = string.Empty;
}