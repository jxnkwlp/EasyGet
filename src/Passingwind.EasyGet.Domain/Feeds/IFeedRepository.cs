using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Passingwind.EasyGet.Feeds;

public interface IFeedRepository : IRepository<Feed, Guid>
{
    Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default);

    Task<List<Feed>> GetListAsync(string filter, bool includeDetails = false, CancellationToken cancellationToken = default);

    Task<List<Feed>> GetPagedListAsync(int skipCount, int maxResultCount, string filter, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default);

    Task<Feed> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
