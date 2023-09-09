using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource#search-result
/// See the section about each item in the <c>versions</c> array.
/// </summary>
public class V3SearchVersion
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("downloads")]
    public long Downloads { get; set; }

    [JsonPropertyName("@id")]
    public string AtId { get; set; } = null!;
}
