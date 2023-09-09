using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class PackageDependency
{
    [JsonPropertyName("@id")]
    public string? Url { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("range")]
    // [JsonConverter(typeof(PackageDependencyRangeConverter))]
    public string? Range { get; set; }
}
