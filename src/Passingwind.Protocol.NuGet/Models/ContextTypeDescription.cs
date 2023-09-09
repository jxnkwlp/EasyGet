using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class ContextTypeDescription
{
    [JsonPropertyName("@id")]
    public string? Id { get; set; }

    [JsonPropertyName("@container")]
    public string? Container { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }
}
