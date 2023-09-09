using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public abstract class BasePackageDependencyGroup<TDependency> where TDependency : PackageDependency
{
    [JsonPropertyName("@id")]
    public string? Url { get; set; }

    [JsonPropertyName("@type")]
    public string? Type { get; set; }

    [JsonPropertyName("dependencies")]
    public List<TDependency> Dependencies { get; set; } = null!;

    [JsonPropertyName("targetFramework")]
    public string? TargetFramework { get; set; }
}
