using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource#search-result
/// </summary>
public class V3SearchPackageType
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
