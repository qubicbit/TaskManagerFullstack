using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TestRunner.Models;

namespace TestRunner;

public class ApiClient
{
    private readonly HttpClient _http;

    public HttpClient HttpClient => _http;

    public ApiClient(string baseUrl)
    {
        _http = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        })
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var payload = new LoginRequest
        {
            Email = email,
            Password = password
        };

        var response = await _http.PostAsJsonAsync("/api/auth/login", payload);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return "";

        var result = JsonSerializer.Deserialize<LoginResponse>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", result!.Token);

        return result.Token;
    }

    public async Task<HttpResponseMessage> Send(ApiEndpoint ep)
    {
        return ep.Method.ToUpper() switch
        {
            "GET" => await _http.GetAsync(ep.Url),
            "POST" => await _http.PostAsync(ep.Url, null),
            "PUT" => await _http.PutAsync(ep.Url, null),
            "DELETE" => await _http.DeleteAsync(ep.Url),
            _ => throw new NotSupportedException($"HTTP-metod '{ep.Method}' stöds inte.")
        };
    }
}
