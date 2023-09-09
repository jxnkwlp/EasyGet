using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class PackageDetailsCatalogLeaf : CatalogLeaf
{
    [JsonPropertyName("authors")]
    public string? Authors { get; set; }

    [JsonPropertyName("copyright")]
    public string? Copyright { get; set; }

    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }

    [JsonPropertyName("lastEdited")]
    public DateTimeOffset LastEdited { get; set; }

    [JsonPropertyName("dependencyGroups")]
    public List<PackageDependencyGroup>? DependencyGroups { get; set; }

    [JsonPropertyName("deprecation")]
    public PackageDeprecation? Deprecation { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("iconUrl")]
    public string? IconUrl { get; set; }

    /// <summary>
    /// Note that an old bug in the NuGet.org catalog had this wrong in some cases.
    /// Example: https://api.nuget.org/v3/catalog0/data/2016.03.11.21.02.55/mvid.fody.2.json
    /// </summary>
    [JsonPropertyName("isPrerelease")]
    public bool IsPrerelease { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("licenseUrl")]
    public string? LicenseUrl { get; set; }

    [JsonPropertyName("listed")]
    public bool? Listed { get; set; }

    [JsonPropertyName("minClientVersion")]
    public string? MinClientVersion { get; set; }

    [JsonPropertyName("packageEntries")]
    public List<PackageEntry>? PackageEntries { get; set; }

    [JsonPropertyName("packageHash")]
    public string? PackageHash { get; set; }

    [JsonPropertyName("packageHashAlgorithm")]
    public string? PackageHashAlgorithm { get; set; }

    [JsonPropertyName("packageSize")]
    public long PackageSize { get; set; }

    [JsonPropertyName("packageTypes")]
    public List<PackageType>? PackageTypes { get; set; }

    [JsonPropertyName("projectUrl")]
    public string? ProjectUrl { get; set; }

    [JsonPropertyName("releaseNotes")]
    public string? ReleaseNotes { get; set; }

    [JsonPropertyName("requireLicenseAcceptance")]
    public bool? RequireLicenseAcceptance { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("verbatimVersion")]
    public string? VerbatimVersion { get; set; }

    [JsonPropertyName("licenseExpression")]
    public string? LicenseExpression { get; set; }

    [JsonPropertyName("licenseFile")]
    public string? LicenseFile { get; set; }

    [JsonPropertyName("iconFile")]
    public string? IconFile { get; set; }

    [JsonPropertyName("readmeFile")]
    public string? ReadmeFile { get; set; }

    [JsonPropertyName("vulnerabilities")]
    public List<PackageVulnerability>? Vulnerabilities { get; set; }
}
