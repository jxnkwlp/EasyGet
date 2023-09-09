using System;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#registration-leaf-object-in-a-page
/// </summary>
public class RegistrationLeafItem
{
    public RegistrationLeafItem(string url, string packageContent, RegistrationCatalogEntry catalogEntry, string? type = "Package")
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        PackageContent = packageContent ?? throw new ArgumentNullException(nameof(packageContent));
        CatalogEntry = catalogEntry ?? throw new ArgumentNullException(nameof(catalogEntry));
        Type = type;
    }

    public RegistrationLeafItem()
    {
    }

    [JsonPropertyName("@id")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("commitId")]
    public string? CommitId { get; set; }

    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset? CommitTimestamp { get; set; }

    [JsonPropertyName("catalogEntry")]
    public RegistrationCatalogEntry CatalogEntry { get; set; } = null!;

    [JsonPropertyName("packageContent")]
    public string PackageContent { get; set; } = null!;

    [JsonPropertyName("registration")]
    public string? Registration { get; set; }
}
