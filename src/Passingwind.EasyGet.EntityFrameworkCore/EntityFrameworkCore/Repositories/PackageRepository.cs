using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Passingwind.EasyGet.Feeds;
using Passingwind.EasyGet.Packages;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Passingwind.EasyGet.EntityFrameworkCore.Repositories;

public class PackageRepository : EfCoreRepository<EasyGetDbContext, Package, Guid>, IPackageRepository
{
    public PackageRepository(IDbContextProvider<EasyGetDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    //public async Task<Package> EnsureAsync(Guid feedId, string name, CancellationToken cancellationToken = default)
    //{
    //    var dbset = await GetDbSetAsync();
    //    // var feedDbset = (await GetDbContextAsync()).Set<Feed>();

    //    var entity = await dbset.FirstOrDefaultAsync(x => x.FeedId == feedId && x.Name == name, cancellationToken);

    //    if (entity != null)
    //    {
    //        return entity;
    //    }

    //    entity = new Package(GuidGenerator.Create(), feedId, FeedType.Unknow, name.ToLowerInvariant(), CurrentTenant.Id);

    //    await dbset.AddAsync(entity);

    //    return entity;
    //}

    public async Task<Package> GetAsync(Guid feedId, string name, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();
        var entity = await dbset.FirstOrDefaultAsync(x => x.Name == name);

        if (entity == null)
            throw new EntityNotFoundException(typeof(Package));

        return entity;
    }

    public async Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();
        return await dbset
            // TODO
            //.WhereIf(!string.IsNullOrEmpty(filter),  )
            .LongCountAsync(cancellationToken);
    }

    public async Task<List<Package>> GetListAsync(string filter, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();
        return await dbset
            // TODO
            //.IncludeIf(includeDetails, TODO )
            //.WhereIf(!string.IsNullOrEmpty(filter), TODO )
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Package>> GetPagedListAsync(int skipCount, int maxResultCount, string filter, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();
        return await dbset
            // TODO
            //.IncludeIf(includeDetails, TODO )
            //.WhereIf(!string.IsNullOrEmpty(filter), TODO )
            .PageBy(skipCount, maxResultCount)
            .OrderBy(sorting ?? nameof(Package.CreationTime) + " desc")
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsExistsAsync(Guid feedId, string name, CancellationToken cancellationToken = default)
    {
        var dbset = await GetDbSetAsync();

        return await dbset.AnyAsync(x => x.FeedId == feedId && x.Name == name, cancellationToken);
    }
}
