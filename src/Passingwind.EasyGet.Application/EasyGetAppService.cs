using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Passingwind.EasyGet.Feeds;
using Passingwind.EasyGet.Localization;
using Passingwind.EasyGet.Packages;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Passingwind.EasyGet;

/* Inherit your application services from this class.
 */
public abstract class EasyGetAppService : ApplicationService
{
    protected IFeedRepository FeedRepository => LazyServiceProvider.LazyGetRequiredService<IFeedRepository>();
    protected FeedDomainService FeedDomainService => LazyServiceProvider.LazyGetRequiredService<FeedDomainService>();

    protected IPackageRepository  PackageRepository => LazyServiceProvider.LazyGetRequiredService<IPackageRepository>();
    protected PackageDomainService PackageDomainService => LazyServiceProvider.LazyGetRequiredService<PackageDomainService>();

    protected IConfiguration Configuration => LazyServiceProvider.GetRequiredService<IConfiguration>();

    protected EasyGetAppService()
    {
        LocalizationResource = typeof(EasyGetResource);
    }

    protected async Task<Feed> GetNuGetFeedByNameAsync(string name)
    {
        var feed = await FeedRepository.GetByNameAsync(name);

        if (feed.Type != FeedType.NuGet)
        {
            throw new UserFriendlyException("The feed type is not NuGet.");
        }

        return feed;
    }
}
