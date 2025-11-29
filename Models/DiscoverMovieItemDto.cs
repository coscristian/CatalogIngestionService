using Newtonsoft.Json;

namespace CatalogIngestionService.Models;

public class DiscoverMovieItemDto
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "adult")]
    public bool IsAdult { get; set; }
    
    [JsonProperty(PropertyName = "backdrop_path")]
    public string BackdropPath { get; set; } =  string.Empty;
    
    [JsonProperty(PropertyName = "genre_ids")]
    public IEnumerable<int> GenreIds { get; set; } = [];
    
    [JsonProperty(PropertyName = "original_language")]
    public string OriginalLanguage { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "original_title")]
    public string OriginalTitle { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "overview")]
    public string Overview { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "popularity")]
    public decimal Popularity { get; set; }
    
    [JsonProperty(PropertyName = "poster_path")]
    public string PosterPath { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "release_date")]
    public string ReleaseDate { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonProperty(PropertyName = "video")]
    public bool HasVideo { get; set; }
    
    [JsonProperty(PropertyName = "vote_average")]
    public decimal VoteAverage { get; set; }
    
    [JsonProperty(PropertyName = "vote_count")]
    public int VoteCount { get; set; }
    
    
}