using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Passingwind.EasyGet.Feeds;
public class FeedDomainService : DomainService
{
    public Task<string> GetBaseUrlAsync(Feed feed, string urlPrefix)
    {
        var url = $"{urlPrefix.EnsureEndsWith('/')}f/{feed.Type.ToString().ToLowerInvariant()}/{feed.Name}";

        return Task.FromResult(url);
    }
}
