using System.Text.Json.Serialization;

namespace TestRunner.Models;

public class SwaggerDoc
{
    [JsonPropertyName("paths")]
    public Dictionary<string, SwaggerPathItem> Paths { get; set; } = new();
}

public class SwaggerPathItem
{
    [JsonPropertyName("get")]
    public SwaggerOperation? Get { get; set; }

    [JsonPropertyName("post")]
    public SwaggerOperation? Post { get; set; }

    [JsonPropertyName("put")]
    public SwaggerOperation? Put { get; set; }

    [JsonPropertyName("delete")]
    public SwaggerOperation? Delete { get; set; }
}

public class SwaggerOperation
{
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
}
