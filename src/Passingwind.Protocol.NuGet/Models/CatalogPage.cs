using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class CatalogPage
{
    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset CommitTimestamp { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("parent")]
    public string? Parent { get; set; }

    [JsonPropertyName("items")]
    public List<CatalogLeafItem>? Items { get; set; }

    [JsonPropertyName("@context")]
    public CatalogPageContext? Context { get; set; }
}
