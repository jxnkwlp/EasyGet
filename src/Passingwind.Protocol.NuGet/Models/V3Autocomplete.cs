using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/search-autocomplete-service-resource#response
/// </summary>
public class V3Autocomplete
{
    [JsonPropertyName("totalHits")]
    public long? TotalHits { get; set; }

    [JsonPropertyName("data")]
    public List<string> Data { get; set; } = null!;
}
