using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class V3SearchAlternatePackage
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("range")]
    public string Range { get; set; } = null!;
}
