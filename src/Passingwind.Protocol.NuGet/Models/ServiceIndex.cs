using System.Collections.Generic;

namespace Passingwind.Protocol.NuGet.Models;

/// <summary>
///  Source: https://learn.microsoft.com/zh-cn/nuget/api/service-index
/// </summary>
public class ServiceIndex
{
    public string Version { get; set; } = null!;

    public List<ServiceIndexResource> Resources { get; set; } = null!;
}
