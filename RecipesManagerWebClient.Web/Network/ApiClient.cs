using System.Net.Http.Headers;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json;

namespace RecipesManagerWebClient.Web.Network;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    private readonly GraphQLHttpClient _graphQLClient;

    private readonly AuthenticationService _authenticationService;

    public ApiClient(
        IHttpClientFactory httpClientFactory,
        GraphQLHttpClient graphQLClient,
        AuthenticationService authenticationService)
    {
        _httpClient = httpClientFactory.CreateClient("ApiHttpClient");
        _graphQLClient = graphQLClient;
        _authenticationService = authenticationService;
    }

    public async Task<dynamic> QueryAsync(GraphQLRequest request) {
        await SetAuthenticationAsync();

        return await _graphQLClient.SendQueryAsync<dynamic>(request);
    }

    public async Task<T> QueryAsync<T>(GraphQLRequest request, string propertyName) {
        await SetAuthenticationAsync();

        var response = await _graphQLClient.SendQueryAsync<dynamic>(request);
        var obj = response.Data.GetValue(propertyName);
        var jsonResponse = JsonConvert.SerializeObject(obj);

        var model = JsonConvert.DeserializeObject<T>(jsonResponse);
        return model;
    }

    public async Task<T> PostFormAsync<T>(string url, MultipartFormDataContent formData) {
        var response = await _httpClient.PostAsync(url, formData);
        var responseContent = await response.Content.ReadAsStringAsync();

        var model = JsonConvert.DeserializeObject<T>(responseContent);
        return model;
    }

    public async Task<T> PostJsonAsync<T>(string url, Object obj) {
        var response = await _httpClient.PostAsJsonAsync(url, obj);
        var responseContent = await response.Content.ReadAsStringAsync();

        var model = JsonConvert.DeserializeObject<T>(responseContent);
        return model;
    }

    public async Task<T> PutFormAsync<T>(string url, MultipartFormDataContent formData) {
        var response = await _httpClient.PutAsync(url, formData);
        var responseContent = await response.Content.ReadAsStringAsync();

        var model = JsonConvert.DeserializeObject<T>(responseContent);
        return model;
    }

    public async Task<T> PutJsonAsync<T>(string url, Object obj) {
        var response = await _httpClient.PutAsJsonAsync(url, obj);
        var responseContent = await response.Content.ReadAsStringAsync();

        var model = JsonConvert.DeserializeObject<T>(responseContent);
        return model;
    }

    private async Task SetAuthenticationAsync()
    {
        var authToken = await _authenticationService.GetAuthTokenAsync();
        _graphQLClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
    }
}
