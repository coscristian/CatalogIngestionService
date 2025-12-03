using Newtonsoft.Json;

namespace CatalogIngestionService.Models.Language.Response;

public class LanguageIngestStreambitResponseDto
{
    [JsonProperty(PropertyName = "language_id")]
    public int LanguageId { get; set; }

    [JsonProperty(PropertyName = "name")] 
    public string Name { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "english_name")]
    public string EnglishName { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "iso_639_1")]
    public string Iso6391 { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "created_date")]
    public DateTime CreatedDate { get; set; }
    
    [JsonProperty(PropertyName = "last_modified")]
    public DateTime LastModified { get; set; }
}