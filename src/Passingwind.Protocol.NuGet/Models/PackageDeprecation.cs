using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#package-deprecation
/// </summary>
public class PackageDeprecation
{
    [JsonPropertyName("@id")]
    public string? Url { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("alternatePackage")]
    public AlternatePackage? AlternatePackage { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("reasons")]
    public List<string>? Reasons { get; set; }
}
