using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// Source: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#registration-index
/// </summary>
public class RegistrationIndex
{
    public RegistrationIndex()
    {
    }

    public RegistrationIndex(List<RegistrationPage> items)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }

    [JsonPropertyName("@id")]
    public string? Url { get; set; }

    [JsonPropertyName("@type")]
    public List<string>? Types { get; set; }

    [JsonPropertyName("commitId")]
    public string? CommitId { get; set; }

    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset? CommitTimestamp { get; set; }

    [JsonPropertyName("count")]
    public long Count => Items?.Count ?? 0;

    [JsonPropertyName("items")]
    public List<RegistrationPage> Items { get; set; } = null!;

    [JsonPropertyName("@context")]
    public RegistrationContainerContext? Context { get; set; }
}
