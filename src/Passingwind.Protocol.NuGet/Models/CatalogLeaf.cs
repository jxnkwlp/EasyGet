using System;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class CatalogLeaf : ICatalogLeafItem
{
    [JsonPropertyName("@id")]
    public string? Url { get; set; }

    [JsonPropertyName("@type")]
    //[JsonConverter(typeof(CatalogLeafTypeConverter))]
    public CatalogLeafType Type { get; set; }

    [JsonPropertyName("catalog:commitId")]
    public string? CommitId { get; set; }

    [JsonPropertyName("catalog:commitTimeStamp")]
    public DateTimeOffset CommitTimestamp { get; set; }

    [JsonPropertyName("id")]
    public string? PackageId { get; set; }

    [JsonPropertyName("published")]
    public DateTimeOffset Published { get; set; }

    [JsonPropertyName("version")]
    public string? PackageVersion { get; set; }
}
