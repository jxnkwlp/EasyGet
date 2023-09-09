using System;

namespace Passingwind.Protocol.NuGet.Models;

public interface ICatalogLeafItem
{
    string? CommitId { get; }
    DateTimeOffset CommitTimestamp { get; }
    string? PackageId { get; }
    string? PackageVersion { get; }
    CatalogLeafType Type { get; }
}
