using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#registration-leaf
/// </summary>
public class RegistrationLeaf
{
    public RegistrationLeaf(string url)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
    }

    public RegistrationLeaf()
    {
    }

    [JsonPropertyName("@id")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("@type")]
    public List<string>? Types { get; set; }

    [JsonPropertyName("catalogEntry")]
    public string? CatalogEntry { get; set; }

    [JsonPropertyName("listed")]
    public bool? Listed { get; set; }

    [JsonPropertyName("packageContent")]
    public string? PackageContent { get; set; }

    [JsonPropertyName("published")]
    public DateTimeOffset? Published { get; set; }

    [JsonPropertyName("registration")]
    public string? Registration { get; set; }

    [JsonPropertyName("@context")]
    public RegistrationLeafContext? Context { get; set; }
}
