using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Passingwind.EasyGet.Packages;
using Passingwind.EasyGet.Packages.NuGets;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Passingwind.EasyGet.EntityFrameworkCore.Repositories;

public class NuGetPackageRepository : EfCoreRepository<EasyGetDbContext, NuGetPackage, Guid>, INuGetPackageRepository
{
    public NuGetPackageRepository(IDbContextProvider<EasyGetDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<NuGetPackage> FindByIdAsync(Guid feedId, Guid packageId, string version, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset.FirstOrDefaultAsync(x => x.FeedId == feedId && x.PackageId == packageId && x.Version == version, cancellationToken: cancellationToken);
    }

    public async Task<NuGetPackage> FindByNameAsync(Guid feedId, string name, string version, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset.FirstOrDefaultAsync(x => x.FeedId == feedId && x.Name == name && x.Version == version, cancellationToken: cancellationToken);
    }

    public async Task<NuGetPackage> GetByIdAsync(Guid feedId, Guid packageId, string version, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        var entity = await dbset.FirstOrDefaultAsync(x => x.FeedId == feedId && x.PackageId == packageId && x.Version == version, cancellationToken: cancellationToken);

        if (entity == null)
            throw new EntityNotFoundException(typeof(NuGetPackage));

        return entity;
    }

    public async Task<NuGetPackage> GetByNameAsync(Guid feedId, string name, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        var entity = await dbset.FirstOrDefaultAsync(x => x.FeedId == feedId && x.Name == name, cancellationToken: cancellationToken);

        if (entity == null)
            throw new EntityNotFoundException(typeof(NuGetPackage));

        return entity;
    }

    public async Task<NuGetPackage> GetByNameAsync(Guid feedId, string name, string version, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        var entity = await dbset.FirstOrDefaultAsync(x => x.FeedId == feedId && x.Name == name && x.Version == version, cancellationToken: cancellationToken);

        if (entity == null)
            throw new EntityNotFoundException(typeof(NuGetPackage));

        return entity;
    }

    public async Task<long> GetCountAsync(Guid feedId, string filter = null, string name = null, bool? includePrerelease = null, bool? isSemVer2 = null, string packageType = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId)
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name == name)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.Contains(filter) || x.Title.Contains(filter))
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .WhereIf(!string.IsNullOrWhiteSpace(packageType), x => x.PackageTypes.Any(t => t.PackageType == packageType))
            .LongCountAsync(cancellationToken);
    }

    public async Task<List<NuGetPackage>> GetLatestVersionListByNamesAsync(Guid feedId, IEnumerable<string> names, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId && names.Contains(x.Name) && x.IsLatest)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<NuGetPackage>> GetLatestVersionListByPackageIdsAsync(Guid feedId, IEnumerable<Guid> packageIds, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId && packageIds.Contains(x.PackageId) && x.IsLatest)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<NuGetPackage>> GetListAsync(Guid feedId, string filter = null, string name = null, bool? includePrerelease = null, bool? isSemVer2 = null, string packageType = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId)
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name == name)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.Contains(filter) || x.Title.Contains(filter))
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .WhereIf(!string.IsNullOrWhiteSpace(packageType), x => x.PackageTypes.Any(t => t.PackageType == packageType))
            .ToListAsync(cancellationToken);
    }

    public async Task<long> GetPackageCountAsync(Guid feedId, string filter = null, string name = null, bool? includePrerelease = null, bool? isSemVer2 = null, string packageType = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId)
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name == name)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.Contains(filter) || x.Title.Contains(filter))
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .WhereIf(!string.IsNullOrWhiteSpace(packageType), x => x.PackageTypes.Any(t => t.PackageType == packageType))
            .Select(x => x.PackageId)
            .Distinct()
            .LongCountAsync(cancellationToken);
    }

    public async Task<List<PackageInfo>> GetPackageInfoListAsync(Guid packageId, string name, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.PackageId == packageId && x.Name == name)
            .Select(x => new PackageInfo()
            {
                Name = x.Name,
                Version = x.Version,
                DownloadCount = x.DownloadCount,
                IconUrl = x.IconUrl,
                Size = x.Size,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PackageInfo> GetPackageInfoListAsync(Guid packageId, string name, string version, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        var entity = await dbset.FirstOrDefaultAsync(x => x.PackageId == packageId && x.Name == name && x.Version == version, cancellationToken);

        return new PackageInfo()
        {
            Name = entity.Name,
            Version = entity.Version,
            DownloadCount = entity.DownloadCount,
            IconUrl = entity.IconUrl,
            Size = entity.Size,
        };
    }

    public async Task<List<Package>> GetPackageListAsync(Guid feedId, string filter = null, string name = null, bool? includePrerelease = null, bool? isSemVer2 = null, string packageType = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId)
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name == name)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.Contains(filter) || x.Title.Contains(filter))
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .WhereIf(!string.IsNullOrWhiteSpace(packageType), x => x.PackageTypes.Any(t => t.PackageType == packageType))
            .Select(x => x.Package)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Package>> GetPackagePagedListAsync(int skipCount, int maxResultCount, Guid feedId, string filter = null, string name = null, bool? includePrerelease = null, bool? isSemVer2 = null, string packageType = null, string sorting = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();
        return await dbset
            .Where(x => x.FeedId == feedId)
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name == name)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.Contains(filter) || x.Title.Contains(filter))
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .WhereIf(!string.IsNullOrWhiteSpace(packageType), x => x.PackageTypes.Any(t => t.PackageType == packageType))
            .Select(x => x.Package)
            .Distinct()
            .OrderBy(sorting ?? nameof(NuGetPackage.CreationTime) + " desc")
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<NuGetPackage>> GetPagedListAsync(int skipCount, int maxResultCount, Guid feedId, string filter = null, string name = null, bool? includePrerelease = null, bool? isSemVer2 = null, string packageType = null, string sorting = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();
        return await dbset
            .Where(x => x.FeedId == feedId)
            .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name == name)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.Name.Contains(filter) || x.Title.Contains(filter))
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .WhereIf(!string.IsNullOrWhiteSpace(packageType), x => x.PackageTypes.Any(t => t.PackageType == packageType))
            .OrderBy(sorting ?? nameof(NuGetPackage.CreationTime) + " desc")
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetVersionsAsync(Guid feedId, string name, bool? includePrerelease = null, bool? isSemVer2 = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId && x.Name == name)
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .OrderBy(nameof(NuGetPackage.CreationTime) + " desc")
            .Select(x => x.Version)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetVersionsAsync(Guid feedId, Guid packageId, bool? includePrerelease = null, bool? isSemVer2 = null, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset
            .Where(x => x.FeedId == feedId && x.PackageId == packageId)
            .WhereIf(includePrerelease.HasValue && includePrerelease != true, x => !x.Prerelease)
            .WhereIf(isSemVer2.HasValue, x => x.IsSemVer2 == isSemVer2)
            .OrderBy(nameof(NuGetPackage.CreationTime) + " desc")
            .Select(x => x.Version)
            .ToListAsync(cancellationToken);
    }

    public async Task IncrementDownloadsAsync(Guid feedId, Guid packageId, string version, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        await dbset.Where(x => x.FeedId == feedId && x.PackageId == packageId && x.Version == version).ExecuteUpdateAsync(x => x.SetProperty(s => s.DownloadCount, s => s.DownloadCount + 1), cancellationToken: cancellationToken);
    }

    public async Task SetIsLatestAsync(Guid feedId, Guid packageId, string version, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        await dbset.Where(x => x.FeedId == feedId && x.PackageId == packageId && x.Version == version).ExecuteUpdateAsync(x => x.SetProperty(s => s.IsLatest, s => true), cancellationToken: cancellationToken);

        await dbset.Where(x => x.FeedId == feedId && x.PackageId == packageId && x.Version != version).ExecuteUpdateAsync(x => x.SetProperty(s => s.IsLatest, s => false), cancellationToken: cancellationToken);
    }

    public async Task UnsetIsLatestAsync(Guid feedId, Guid packageId, Guid excludeId, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        await dbset.Where(x => x.FeedId == feedId && x.PackageId == packageId && x.Id == excludeId).ExecuteUpdateAsync(x => x.SetProperty(s => s.IsLatest, s => false), cancellationToken: cancellationToken);
    }
}
