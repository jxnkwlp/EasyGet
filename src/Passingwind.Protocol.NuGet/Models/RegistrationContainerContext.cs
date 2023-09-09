using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class RegistrationContainerContext
{
    [JsonPropertyName("@vocab")]
    public string? Vocab { get; set; }

    [JsonPropertyName("catalog")]
    public string? Catalog { get; set; }

    [JsonPropertyName("xsd")]
    public string? Xsd { get; set; }

    [JsonPropertyName("items")]
    public ContextTypeDescription? Items { get; set; }

    [JsonPropertyName("commitTimeStamp")]
    public ContextTypeDescription? CommitTimestamp { get; set; }

    [JsonPropertyName("commitId")]
    public ContextTypeDescription? CommitId { get; set; }

    [JsonPropertyName("count")]
    public ContextTypeDescription? Count { get; set; }

    [JsonPropertyName("parent")]
    public ContextTypeDescription? Parent { get; set; }

    [JsonPropertyName("tags")]
    public ContextTypeDescription? Tags { get; set; }

    [JsonPropertyName("reasons")]
    public ContextTypeDescription? Reasons { get; set; }

    [JsonPropertyName("packageTargetFrameworks")]
    public ContextTypeDescription? PackageTargetFrameworks { get; set; }

    [JsonPropertyName("dependencyGroups")]
    public ContextTypeDescription? DependencyGroups { get; set; }

    [JsonPropertyName("dependencies")]
    public ContextTypeDescription? Dependencies { get; set; }

    [JsonPropertyName("packageContent")]
    public ContextTypeDescription? PackageContent { get; set; }

    [JsonPropertyName("published")]
    public ContextTypeDescription? Published { get; set; }

    [JsonPropertyName("registration")]
    public ContextTypeDescription? Registration { get; set; }

    [JsonPropertyName("vulnerabilities")]
    public ContextTypeDescription? Vulnerabilities { get; set; }
}
