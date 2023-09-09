using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class V3SearchDeprecation
{
    [JsonPropertyName("alternatePackage")]
    public V3SearchAlternatePackage? AlternatePackage { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("reasons")]
    public string[]? Reasons { get; set; }
}
