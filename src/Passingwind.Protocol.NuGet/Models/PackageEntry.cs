using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class PackageEntry
{
    [JsonPropertyName("@id")]
    public string? Url { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("compressedLength")]
    public string? CompressedLength { get; set; }

    [JsonPropertyName("length")]
    public string? Length { get; set; }
}
