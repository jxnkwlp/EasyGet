using System;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class CatalogPageItem
{
    [JsonPropertyName("@id")]
    public string? Url { get; set; }

    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset CommitTimestamp { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}
