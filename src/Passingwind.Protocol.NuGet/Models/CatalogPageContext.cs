using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class CatalogPageContext
{
    [JsonPropertyName("@vocab")]
    public string? Vocab { get; set; }

    [JsonPropertyName("nuget")]
    public string? NuGet { get; set; }

    [JsonPropertyName("@items")]
    public ContextTypeDescription? Items { get; set; }

    [JsonPropertyName("parent")]
    public ContextTypeDescription? Parent { get; set; }

    [JsonPropertyName("commitTimeStamp")]
    public ContextTypeDescription? CommitTimestamp { get; set; }

    [JsonPropertyName("nuget:lastCreated")]
    public ContextTypeDescription? LastCreated { get; set; }

    [JsonPropertyName("nuget:lastEdited")]
    public ContextTypeDescription? LastEdited { get; set; }

    [JsonPropertyName("nuget:lastDeleted")]
    public ContextTypeDescription? LastDeleted { get; set; }
}
