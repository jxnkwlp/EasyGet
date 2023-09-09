using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Passingwind.Protocol.NuGet.Models;
using Volo.Abp.DependencyInjection;

namespace Passingwind.EasyGet.NuGets;

public class NuGetClient : LegacyLoggerAdapter, INuGetClient, ISingletonDependency
{
    private static readonly SourceCacheContext Cache = new SourceCacheContext();

    protected ILogger<NuGetClient> Logger { get; }

    public NuGetClient(ILogger<NuGetClient> logger)
    {
        Logger = logger;
    }

    #region Log

    public override void LogDebug(string data)
    {
        Logger.LogDebug(data);
    }

    public override void LogVerbose(string data)
    {
        Logger.LogDebug(data);
    }

    public override void LogInformation(string data)
    {
        Logger.LogInformation(data);
    }

    public override void LogMinimal(string data)
    {
        Logger.LogInformation(data);
    }

    public override void LogWarning(string data)
    {
        Logger.LogWarning(data);
    }

    public override void LogError(string data)
    {
        Logger.LogError(data);
    }

    public override void LogInformationSummary(string data)
    {
        Logger.LogInformation(data);
    }

    #endregion Log

    public async Task<Stream> GetNupkgStreamAsync(string serviceUrl, string id, string version, CancellationToken cancellationToken = default)
    {
        SourceRepository repository = Repository.Factory.GetCoreV3(serviceUrl);
        FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

        NuGetVersion packageVersion = new NuGetVersion(version);

        MemoryStream packageStream = new MemoryStream();

        var result = await resource.CopyNupkgToStreamAsync(
             id,
             packageVersion,
             packageStream,
             Cache,
             this,
             cancellationToken);

        return result ? packageStream : null;
    }

    public async Task<IReadOnlyList<string>> GetVersionsAsync(string serviceUrl, string id, CancellationToken cancellationToken = default)
    {
        SourceRepository repository = Repository.Factory.GetCoreV3(serviceUrl);
        FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

        var list = await resource.GetAllVersionsAsync(id, Cache, this, cancellationToken);

        return list.Select(x => x.ToNormalizedString()).ToList();
    }

    public async Task<IReadOnlyList<RegistrationCatalogEntry>> GetRegistrationCatalogEntriesAsync(string serviceUrl, string id, CancellationToken cancellationToken = default)
    {
        SourceRepository repository = Repository.Factory.GetCoreV3(serviceUrl);
        var resource = await repository.GetResourceAsync<RegistrationResourceV3>();

        var list = await resource.GetPackageMetadata(id, true, false, Cache, this, cancellationToken);

        var result = new List<RegistrationCatalogEntry>();

        foreach (var item in list)
        {
            result.Add(JsonSerializer.Deserialize<RegistrationCatalogEntry>(item.ToString(Newtonsoft.Json.Formatting.None)));
        }

        return result;
    }

    //public async Task<RegistrationCatalogEntry> GetRegistrationCatalogEntriesAsync(string serviceUrl, string id, string version, CancellationToken cancellationToken = default)
    //{
    //    SourceRepository repository = Repository.Factory.GetCoreV3(serviceUrl);
    //    var resource = await repository.GetResourceAsync<RegistrationResourceV3>();

    //    var list = await resource.GetPackageMetadata(id, version, true, false, Cache, this, cancellationToken);

    //    var result = new List<RegistrationCatalogEntry>();

    //    foreach (var item in list)
    //    {
    //        var catalogEntry = JsonSerializer.Deserialize<RegistrationCatalogEntry>(item.ToString());
    //        result.Add(catalogEntry);
    //    }

    //    return result;
    //}
}
