using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NuGet.Versioning;
using Passingwind.EasyGet.Exceptions;
using Passingwind.EasyGet.Feeds;
using Passingwind.EasyGet.NuGets;
using Passingwind.EasyGet.Packages;
using Passingwind.EasyGet.Packages.NuGets;
using Passingwind.Protocol.NuGet.Models;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;

namespace Passingwind.EasyGet.Protocols.NuGets;

public class NuGetV3AppService : EasyGetAppService, INuGetV3AppService
{
    private readonly INuGetPackageSearchProvider _nuGetPackageSearchProvider;
    private readonly INuGetPackageRepository _nuGetPackageRepository;
    private readonly INuGetPackageStorage _nuGetPackageStorage;
    private readonly INuGetPackageManager _nuGetPackageManager;
    private readonly INuGetClient _nuGetClient;

    public NuGetV3AppService(
        INuGetPackageSearchProvider nuGetPackageSearchProvider,
        INuGetPackageRepository nuGetPackageRepository,
        INuGetPackageStorage nuGetPackageStorage,
        INuGetPackageManager nuGetPackageManager,
        INuGetClient nuGetClient = null)
    {
        _nuGetPackageSearchProvider = nuGetPackageSearchProvider;
        _nuGetPackageRepository = nuGetPackageRepository;
        _nuGetPackageStorage = nuGetPackageStorage;
        _nuGetPackageManager = nuGetPackageManager;
        _nuGetClient = nuGetClient;
    }

    public virtual async Task<NuGetV3ServiceIndexDto> GetAsync(string feedName)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        var dto = new NuGetV3ServiceIndexDto()
        {
            Version = "3.0.0",
            Resources = new List<ServiceIndexResource>(),
        };

        var appUrl = Configuration.GetValue<string>("App:SelfUrl");
        var baseUrl = await GetFeedUrlPathPrefixAsync(feed);

        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.SearchBaseUrl, baseUrl), "SearchQueryService", "Query endpoint of NuGet Search service (primary)"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.SearchBaseUrl, baseUrl), "SearchQueryService/3.0.0-beta", "Query endpoint of NuGet Search service (primary)"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.SearchBaseUrl, baseUrl), "SearchQueryService/3.0.0-rc", "Query endpoint of NuGet Search service (primary)"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.SearchBaseUrl, baseUrl), "SearchQueryService/3.5.0", "Query endpoint of NuGet Search service (primary) that supports package type filtering"));

        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.AutocompleteBaseUrl, baseUrl), "SearchAutocompleteService", "Autocomplete endpoint of NuGet Search service (primary)"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.AutocompleteBaseUrl, baseUrl), "SearchAutocompleteService/3.0.0-beta", "Autocomplete endpoint of NuGet Search service (primary)"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.AutocompleteBaseUrl, baseUrl), "SearchAutocompleteService/3.0.0-rc", "Autocomplete endpoint of NuGet Search service (primary)"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.AutocompleteBaseUrl, baseUrl), "SearchAutocompleteService/3.5.0", "Autocomplete endpoint of NuGet Search service (primary) that supports package type filtering"));

        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.PackageBaseUrl, baseUrl), "PackageBaseAddress/3.0.0", $"Base URL of where NuGet packages are stored, the path format is {baseUrl}v3/flatcontainer/{{id-lower}}/{{version-lower}}/{{id-lower}}.{{version-lower}}.nupkg"));

        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.RegistrationsBaseUrl, baseUrl), "RegistrationsBaseUrl", "Base URL NuGet package registration info stored"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.RegistrationsBaseUrl, baseUrl), "RegistrationsBaseUrl/3.0.0-beta", "Base URL NuGet package registration info stored"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.RegistrationsBaseUrl, baseUrl), "RegistrationsBaseUrl/3.0.0-rc", "Base URL NuGet package registration info stored"));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.RegistrationsBaseUrl, baseUrl), "RegistrationsBaseUrl/3.4.0", "Base URL NuGet package registration info stored in GZIP format. This base URL does not include SemVer 2.0.0 packages."));
        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.RegistrationsBaseUrl, baseUrl), "RegistrationsBaseUrl/3.6.0", "Base URL NuGet package registration info stored in GZIP format. This base URL includes SemVer 2.0.0 packages."));

        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.CatalogBaseUrl, baseUrl), "Catalog/3.0.0", "Index of the NuGet package catalog"));

        dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.PackagePublishBaseUrl, baseUrl), "PackagePublish/2.0.0"));
        // dto.Resources.Add(new ServiceIndexResource(string.Format(NuGetServiceUrlFormat.SymbolPackagePublishBaseUrl, baseUrl), "SymbolPackagePublish/4.9.0"));

        dto.Resources.Add(new ServiceIndexResource($"{appUrl}/nuget/{feedName}/packages/{{id}}/{{version}}", "PackageDetailsUriTemplate/5.1.0", "URI template used by NuGet Client to construct details URL for packages"));

        return dto;
    }

    public virtual async Task<NuGetV3RegistrationIndexResultDto> GetRegistrationAsync(string feedName, string id)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        var dto = new NuGetV3RegistrationIndexResultDto();

        if (feed.HasUpStream())
        {
            var upstreamRegistrationCatalogEntries = await _nuGetClient.GetRegistrationCatalogEntriesAsync(feed.Mirror.MirrorUrl, id);

            var splitResult = upstreamRegistrationCatalogEntries.SplitByPeer(64);

            var list = new List<RegistrationPage>();

            var baseUrl = await GetFeedUrlPathPrefixAsync(feed);

            foreach (var item in splitResult)
            {
                string packageRegistrationUrl = string.Format(NuGetServiceUrlFormat.RegistrationIndexUrl, baseUrl, id);

                var versions = item.Value.Select(x => NuGetVersion.Parse(x.Version));

                var t1 = versions.OrderBy(x => x);
                var t2 = versions.OrderByDescending(x => x);

                var lowerVersion = versions.OrderBy(x => x).First();
                var upperVersion = versions.OrderByDescending(x => x).First();

                var registrationPage = new RegistrationPage(packageRegistrationUrl, "catalog:CatalogPage", new List<RegistrationLeafItem>(), lowerVersion.ToNormalizedString(), upperVersion.ToNormalizedString());

                foreach (var packageEntry in item.Value)
                {
                    string packageVersionRegistrationUrl = string.Format(NuGetServiceUrlFormat.RegistrationUrl, baseUrl, id, packageEntry.Version);
                    string packageVersionCatalogUrl = string.Format(NuGetServiceUrlFormat.CatalogUrl, baseUrl, id, packageEntry.Version);
                    string packageContentUrl = string.Format(NuGetServiceUrlFormat.PackageContentUrl, baseUrl, id, packageEntry.Version);

                    registrationPage.Items.Add(new RegistrationLeafItem(packageVersionRegistrationUrl, packageContentUrl, packageEntry)
                    {
                        Registration = packageVersionRegistrationUrl,
                    });
                }

                list.Add(registrationPage);
            }

            dto = new NuGetV3RegistrationIndexResultDto()
            {
                Items = list,
            };
        }
        else
        {
            var package = await PackageRepository.GetAsync(feed.Id, id);

            var list = await _nuGetPackageRepository.GetListAsync(feed.Id, name: package.Name);
            var count = await _nuGetPackageRepository.GetCountAsync(feed.Id, name: package.Name);

            list = list.OrderByDescending(x => x.Version).ToList();

            if (list.Count > 0)
            {
                var lowerVersion = list.Last().Version;
                var upperVersion = list[0].Version;

                var registrationItems = await Task.WhenAll(list.Select(async x => await ToRegistrationPage(feed, x)));

                var baseUrl = await GetFeedUrlPathPrefixAsync(feed);
                string packageRegistrationUrl = string.Format(NuGetServiceUrlFormat.RegistrationIndexUrl, baseUrl, package.NameId);

                var registrationPage = new RegistrationPage(packageRegistrationUrl, "catalog:CatalogPage", registrationItems.ToList(), lowerVersion, upperVersion)
                {
                    Parent = packageRegistrationUrl,
                };

                dto = new NuGetV3RegistrationIndexResultDto()
                {
                    Items = new List<RegistrationPage> { registrationPage }
                };
            }
        }

        return dto;
    }

    public virtual async Task<NuGetV3PackageRegistrationResultDto> GetRegistrationByVersionAsync(string feedName, string id, string version)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        var package = await PackageDomainService.GetOrCreateAsync(feed, id, version);

        var packageVersion = await _nuGetPackageRepository.FindByNameAsync(feed.Id, package.Name, version);

        if (packageVersion == null && feed.HasUpStream())
        {
            packageVersion = await _nuGetPackageManager.CreatePackageVersionFromMirrorAsync(feed, package, version);
        }

        if (packageVersion == null)
        {
            throw new EntityNotFoundException();
        }

        var baseUrl = await GetFeedUrlPathPrefixAsync(feed);

        string packageRegistrationUrl = string.Format(NuGetServiceUrlFormat.RegistrationIndexUrl, baseUrl, packageVersion.NameId);
        string packageVersionRegistrationUrl = string.Format(NuGetServiceUrlFormat.RegistrationUrl, baseUrl, packageVersion.NameId, packageVersion.Version);
        string packageVersionCatalogUrl = string.Format(NuGetServiceUrlFormat.CatalogUrl, baseUrl, packageVersion.NameId, packageVersion.Version);
        string packageContentUrl = string.Format(NuGetServiceUrlFormat.PackageContentUrl, baseUrl, packageVersion.NameId, packageVersion.Version);

        return new NuGetV3PackageRegistrationResultDto()
        {
            Listed = packageVersion.Listed,
            Published = packageVersion.CreationTime,
            Url = packageVersionRegistrationUrl,
            PackageContent = packageContentUrl,
            Registration = packageRegistrationUrl,
            CatalogEntry = packageVersionCatalogUrl,
        };
    }

    public virtual async Task<string> GetPackageSpecAsync(string feedName, string id, string version)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        var package = await PackageDomainService.GetOrCreateAsync(feed, id, version);

        var packageVersion = await _nuGetPackageRepository.FindByNameAsync(feed.Id, package.Name, version);

        if (packageVersion == null && feed.HasUpStream())
        {
            packageVersion = await _nuGetPackageManager.CreatePackageVersionFromMirrorAsync(feed, package, version);
        }

        var content = await _nuGetPackageManager.GetSpecAsync(feed, package, version);

        if (string.IsNullOrWhiteSpace(content))
            throw new StreamNullException();

        return content;
    }

    public virtual async Task<IRemoteStreamContent> GetPackageStreamAsync(string feedName, string id, string version)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        var package = await PackageDomainService.GetOrCreateAsync(feed, id, version);

        var packageVersion = await _nuGetPackageRepository.FindByNameAsync(feed.Id, package.Name, version);

        if (packageVersion == null && feed.HasUpStream())
        {
            packageVersion = await _nuGetPackageManager.CreatePackageVersionFromMirrorAsync(feed, package, version);
        }

        if (packageVersion == null)
        {
            throw new StreamNullException();
        }

        //packageVersion.DownloadCount++;
        //await _nuGetPackageRepository.UpdateAsync(packageVersion);

        //package.DownloadCount++;
        //await PackageRepository.UpdateAsync(package);

        await CurrentUnitOfWork.SaveChangesAsync();

        var stream = await _nuGetPackageManager.GetNupkgStreamAsync(feed, package, version);

        if (stream == null)
            throw new StreamNullException();

        return new RemoteStreamContent(stream, $"{id}.{version}.nupkg", "application/octet-stream");
    }

    public virtual async Task<NuGetV3VersionResultDto> GetVersionsAsync(string feedName, string id)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        // always load versions from upstream
        if (feed.HasUpStream())
        {
            var versions = await _nuGetClient.GetVersionsAsync(feed.Mirror.MirrorUrl, id);

            if (versions?.Any() != true)
            {
                throw new UpstreamNotExistException();
            }

            return new NuGetV3VersionResultDto()
            {
                Versions = versions.ToList(),
            };
        }

        var package = await PackageRepository.GetAsync(feed.Id, id);

        var list = await _nuGetPackageRepository.GetPackageInfoListAsync(package.Id, package.Name);

        return new NuGetV3VersionResultDto()
        {
            Versions = list.ConvertAll(x => x.Version),
        };
    }

    public virtual async Task<NuGetV3SearchAutoCompleteResultDto> SearchAutoCompleteAsync(string feedName, NuGetV3SearchAutoCompleteRequestDto input)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        if (input.Take <= 0) input.Take = 20;

        NuGetVersion semVersion = null;
        if (!string.IsNullOrWhiteSpace(input.SemVerLevel))
        {
            semVersion = NuGetVersion.Parse(input.SemVerLevel);
        }

        if (!string.IsNullOrWhiteSpace(input.Id))
        {
            var package = await PackageRepository.GetAsync(feed.Id, input.Id);

            var list = await _nuGetPackageRepository.GetVersionsAsync(feed.Id, name: input.Id, includePrerelease: input.Prerelease, isSemVer2: semVersion?.IsSemVer2);

            return new NuGetV3SearchAutoCompleteResultDto()
            {
                Data = list,
            };
        }
        else
        {
            var count = await _nuGetPackageSearchProvider.GetCountAsync(feed.Id, filter: input.Q, includePrerelease: input.Prerelease, isSemVer2: semVersion?.IsSemVer2, packageType: input.PackageType);

            var list = await _nuGetPackageSearchProvider.GetListAsync(feed.Id, input.Skip, input.Take, filter: input.Q, includePrerelease: input.Prerelease, isSemVer2: semVersion?.IsSemVer2, packageType: input.PackageType);

            return new NuGetV3SearchAutoCompleteResultDto()
            {
                TotalHits = count,
                Data = list.ConvertAll(x => x.Name),
            };
        }
    }

    public virtual async Task<NuGetV3SearchResultDto> SearchQueryAsync(string feedName, NuGetV3SearchRequestDto input)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        if (input.Take <= 0) input.Take = 20;

        NuGetVersion semVersion = null;
        if (!string.IsNullOrWhiteSpace(input.SemVerLevel))
        {
            semVersion = NuGetVersion.Parse(input.SemVerLevel);
        }

        var baseUrl = await GetFeedUrlPathPrefixAsync(feed);

        var count = await _nuGetPackageSearchProvider.GetCountAsync(feed.Id, filter: input.Q, includePrerelease: input.Prerelease, isSemVer2: semVersion?.IsSemVer2, packageType: input.PackageType);

        var list = await _nuGetPackageSearchProvider.GetListAsync(feed.Id, input.Skip, input.Take, filter: input.Q, includePrerelease: input.Prerelease, isSemVer2: semVersion?.IsSemVer2, packageType: input.PackageType);

        var data = new List<V3SearchPackage>();

        var packageIds = list.Select(x => x.Id);
        var packages = await _nuGetPackageRepository.GetLatestVersionListByPackageIdsAsync(feed.Id, packageIds);

        foreach (var item in list)
        {
            var nugetPackage = packages.Find(x => x.Name == item.Name);
            if (nugetPackage == null)
                continue;

            var all = await _nuGetPackageRepository.GetListAsync(feed.Id, name: item.Name);
            var searchPackage = await ToV3SearchPackageAsync(feed, nugetPackage);

            searchPackage.Versions = all.ConvertAll(x => new V3SearchVersion
            {
                Version = x.Version,
                Downloads = x.DownloadCount,
                AtId = string.Format(NuGetServiceUrlFormat.RegistrationUrl, baseUrl, x.NameId, x.Version)
            });

            data.Add(searchPackage);
        }

        return new NuGetV3SearchResultDto
        {
            TotalHits = count,
            Data = data,
        };
    }

    protected virtual async Task<string> GetFeedUrlPathPrefixAsync(Feed feed)
    {
        var appUrl = Configuration.GetValue<string>("App:SelfUrl");
        return await FeedDomainService.GetBaseUrlAsync(feed, appUrl);
    }

    protected virtual async Task<V3SearchPackage> ToV3SearchPackageAsync(Feed feed, NuGetPackage package)
    {
        var baseUrl = await GetFeedUrlPathPrefixAsync(feed);
        string registration = $"{string.Format(NuGetServiceUrlFormat.RegistrationsBaseUrl, baseUrl)}/{package.NameId}/index.json";
        return new V3SearchPackage(registration, TypeConsts.PackageType, registration, package.Name, package.Version)
        {
            Authors = package.Authors?.Split(','),
            Description = package.Description,
            IconUrl = package.IconUrl,
            LicenseUrl = package.LicenseUrl,
            Owners = package.Owners?.Split(','),
            //PackageTypes = package.PackageTypes,
            ProjectUrl = package.ProjectUrl,
            Summary = package.Summary,
            Tags = package.Tags?.Split(','),
            Title = package.Title,
            TotalDownloads = package.DownloadCount,
            Verified = package.Verified,
            Version = package.Version,
        };
    }

    protected async Task<RegistrationLeafItem> ToRegistrationPage(Feed feed, NuGetPackage package)
    {
        var baseUrl = await GetFeedUrlPathPrefixAsync(feed);
        string packageVersionRegistrationUrl = string.Format(NuGetServiceUrlFormat.RegistrationUrl, baseUrl, package.NameId, package.Version);
        string packageVersionCatalogUrl = string.Format(NuGetServiceUrlFormat.CatalogUrl, baseUrl, package.NameId, package.Version);
        string packageContentUrl = string.Format(NuGetServiceUrlFormat.PackageContentUrl, baseUrl, package.NameId, package.Version);

        return new RegistrationLeafItem(packageVersionCatalogUrl, packageContentUrl, new RegistrationCatalogEntry(packageVersionRegistrationUrl, package.NameId, package.Version));
    }
}
