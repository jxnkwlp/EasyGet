using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
/// This model is used for both the registration page item (found in a registration index) and for a registration
/// page fetched on its own.
/// Source: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#registration-page
/// Source: https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#registration-page-object
/// </summary>
public class RegistrationPage
{
    public RegistrationPage()
    {
    }

    public RegistrationPage(string url, string type, List<RegistrationLeafItem> items, string lower, string upper)
    {
        Url = url;
        Type = type;
        Items = items;
        Lower = lower;
        Upper = upper;
    }

    [JsonPropertyName("@id")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("@type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("commitId")]
    public string? CommitId { get; set; }

    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset? CommitTimestamp { get; set; }

    [JsonPropertyName("count")]
    public int Count => Items?.Count ?? 0;

    /// <summary>
    /// This property can be null when this model is used as an item in <see cref="RegistrationIndex.Items"/> when
    /// the server decided not to inline the leaf items. In this case, the <see cref="Url"/> property can be used
    /// fetch another <see cref="RegistrationPage"/> instance with the <see cref="Items"/> property filled in.
    /// </summary>
    [JsonPropertyName("items")]
    public List<RegistrationLeafItem> Items { get; set; } = new List<RegistrationLeafItem>();

    [JsonPropertyName("parent")]
    public string? Parent { get; set; }

    [JsonPropertyName("lower")]
    public string Lower { get; set; } = null!;

    [JsonPropertyName("upper")]
    public string Upper { get; set; } = null!;

    [JsonPropertyName("@context")]
    public RegistrationContainerContext? Context { get; set; }
}
