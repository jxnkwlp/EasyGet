using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Passingwind.Protocol.NuGet.Models;

namespace Passingwind.EasyGet.NuGets;

public interface INuGetClient
{
    Task<Stream> GetNupkgStreamAsync(string serviceUrl, string id, string version, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<string>> GetVersionsAsync(string serviceUrl, string id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RegistrationCatalogEntry>> GetRegistrationCatalogEntriesAsync(string serviceUrl, string id, CancellationToken cancellationToken = default);
}
