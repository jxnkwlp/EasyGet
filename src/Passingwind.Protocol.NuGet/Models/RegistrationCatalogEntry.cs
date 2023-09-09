using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#catalog-entry
/// </summary>
public class RegistrationCatalogEntry
{
    public RegistrationCatalogEntry()
    {
    }

    public RegistrationCatalogEntry(string url, string packageId, string version)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
        Version = version ?? throw new ArgumentNullException(nameof(version));
    }

    [JsonPropertyName("@id")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("authors")]
    public string? Authors { get; set; }

    [JsonPropertyName("dependencyGroups")]
    public List<RegistrationPackageDependencyGroup>? DependencyGroups { get; set; }

    [JsonPropertyName("deprecation")]
    public PackageDeprecation? Deprecation { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("iconUrl")]
    public string? IconUrl { get; set; }

    [JsonPropertyName("id")]
    public string PackageId { get; set; } = null!;

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("licenseExpression")]
    public string? LicenseExpression { get; set; }

    [JsonPropertyName("licenseUrl")]
    public string? LicenseUrl { get; set; }

    [JsonPropertyName("readmeUrl")]
    public string? ReadmeUrl { get; set; }

    [JsonPropertyName("listed")]
    public bool Listed { get; set; }

    [JsonPropertyName("minClientVersion")]
    public string? MinClientVersion { get; set; }

    [JsonPropertyName("packageContent")]
    public string? PackageContent { get; set; }

    [JsonPropertyName("projectUrl")]
    public string? ProjectUrl { get; set; }

    [JsonPropertyName("published")]
    public DateTimeOffset? Published { get; set; }

    [JsonPropertyName("requireLicenseAcceptance")]
    public bool RequireLicenseAcceptance { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("vulnerabilities")]
    public List<RegistrationPackageVulnerability>? Vulnerabilities { get; set; }
}
