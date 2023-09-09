using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class RegistrationLeafContext
{
    [JsonPropertyName("@vocab")]
    public string? Vocab { get; set; }

    [JsonPropertyName("xsd")]
    public string? Xsd { get; set; }

    [JsonPropertyName("catalogEntry")]
    public ContextTypeDescription? CatalogEntry { get; set; }

    [JsonPropertyName("registration")]
    public ContextTypeDescription? Registration { get; set; }

    [JsonPropertyName("packageContent")]
    public ContextTypeDescription? PackageContent { get; set; }

    [JsonPropertyName("published")]
    public ContextTypeDescription? Published { get; set; }
}
