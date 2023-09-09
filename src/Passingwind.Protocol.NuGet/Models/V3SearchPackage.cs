using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource#search-result
/// </summary>
public class V3SearchPackage
{
    public V3SearchPackage()
    {
    }

    public V3SearchPackage(string id, string type, string registration, string packageId, string version)
    {
        Id = id;
        Type = type;
        Registration = registration;
        PackageId = packageId;
        Version = version;
    }

    [JsonPropertyName("@id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("@type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("registration")]
    public string Registration { get; set; } = null!;

    [JsonPropertyName("id")]
    public string PackageId { get; set; } = null!;

    [JsonPropertyName("version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("iconUrl")]
    public string? IconUrl { get; set; }

    [JsonPropertyName("licenseUrl")]
    public string? LicenseUrl { get; set; }

    [JsonPropertyName("projectUrl")]
    public string? ProjectUrl { get; set; }

    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }

    [JsonPropertyName("authors")]
    public string[]? Authors { get; set; }

    [JsonPropertyName("owners")]
    public string[]? Owners { get; set; }

    [JsonPropertyName("totalDownloads")]
    public long TotalDownloads { get; set; }

    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    [JsonPropertyName("packageTypes")]
    public List<V3SearchPackageType>? PackageTypes { get; set; }

    [JsonPropertyName("versions")]
    public List<V3SearchVersion>? Versions { get; set; }

    [JsonPropertyName("deprecation")]
    public V3SearchDeprecation? Deprecation { get; set; }

    [JsonPropertyName("vulnerabilities")]
    public List<V3SearchVulnerability>? Vulnerabilities { get; set; }
}
