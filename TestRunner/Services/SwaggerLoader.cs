using System.Text.Json;
using TestRunner.Models;

namespace TestRunner.Services;

public class SwaggerLoader
{
    private readonly HttpClient _http;

    public SwaggerLoader(HttpClient http)
    {
        _http = http;
    }

    public async Task<SwaggerDoc> LoadAsync()
    {
        var json = await _http.GetStringAsync("/swagger/v1/swagger.json");

        return JsonSerializer.Deserialize<SwaggerDoc>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}
