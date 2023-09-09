using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
///  Source: https://learn.microsoft.com/zh-cn/nuget/api/service-index#resource
/// </summary>
public class ServiceIndexResource
{
    public ServiceIndexResource(string id, string type, string? comment = null)
    {
        Id = id;
        Type = type;
        Comment = comment;
    }

    public ServiceIndexResource()
    {
    }

    [JsonPropertyName("@id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("@type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
}
