using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CatalogIngestionService;

public abstract class HttpApiClientBase
{
    private readonly HttpClient _client;
    private readonly string _authToken;

    public HttpApiClientBase(HttpClient httpClient, string authToken) 
    {
        _client = httpClient;
        _authToken = authToken;
        
        SetAuthHeader();
        AddJsonAcceptHeader();
    }
    
    private void SetAuthHeader()
    {
        if (!string.IsNullOrWhiteSpace(_authToken))
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _authToken);
        }
    }
    
    private void AddJsonAcceptHeader()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    protected static string BuildUrlWithQueryParams(
        string basePath, Dictionary<string, string> queryParams)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var dict in queryParams)
        {
            query[dict.Key] = dict.Value;
        }
        return string.Join("?", basePath, query.ToString());
    }
    
    protected async Task<T> GetAsync<T>(string apiRoute)
    {
        var response = await _client.GetAsync(apiRoute);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(json))
            throw new InvalidOperationException(
                $"GET {apiRoute} returned empty response but type {typeof(T).Name} was expected."
            );

        var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            // PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        if (result == null)
            throw new JsonException(
                $"Failed to deserialize response from {apiRoute} to type {typeof(T).Name}. Raw content: {json}"
            );

        return result;
    }

    protected async Task<TResponseType> PostAsync<TRequestType, TResponseType>(string apiRoute, TRequestType requestItem)
    {
        var requestBody = new StringContent(
                JsonSerializer.Serialize(requestItem, new JsonSerializerOptions(JsonSerializerDefaults.Web)),
                System.Text.Encoding.UTF8,
                MediaTypeNames.Application.Json);

        var httpResponseMessage = await _client.PostAsync(apiRoute, requestBody);
        httpResponseMessage.EnsureSuccessStatusCode();

        var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<TResponseType>(jsonResponse);
        return result!;
    }
}