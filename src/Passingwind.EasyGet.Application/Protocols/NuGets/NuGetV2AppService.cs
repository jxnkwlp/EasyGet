using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NuGet.Packaging;
using Passingwind.EasyGet.Exceptions;
using Passingwind.EasyGet.Packages;
using Passingwind.EasyGet.Packages.NuGets;
using Volo.Abp.Validation;

namespace Passingwind.EasyGet.Protocols.NuGets;

public class NuGetV2AppService : EasyGetAppService, INuGetV2AppService
{
    private readonly INuGetPackageRepository _nuGetPackageRepository;
    private readonly NuGetPackageDomainService _nuGetPackageDomainService;
    private readonly INuGetPackageManager _nuGetPackageManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NuGetV2AppService(INuGetPackageRepository nuGetPackageRepository, NuGetPackageDomainService nuGetPackageDomainService, INuGetPackageManager nuGetPackageManager, IHttpContextAccessor httpContextAccessor)
    {
        _nuGetPackageRepository = nuGetPackageRepository;
        _nuGetPackageDomainService = nuGetPackageDomainService;
        _nuGetPackageManager = nuGetPackageManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task PublishAsync(string feedName, NuGetV2PackagePublishRequestDto input)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        if (input.Package == null)
        {
            throw new AbpValidationException();
        }

        var fileStream = input.Package?.GetStream();

        if (fileStream == null)
        {
            throw new AbpValidationException();
        }

        fileStream.Seek(0, SeekOrigin.Begin);

        using PackageArchiveReader reader = new PackageArchiveReader(fileStream);
        // TODO Validate
        NuspecReader nuspec = reader.NuspecReader;

        string name = nuspec.GetId();
        var version = nuspec.GetVersion();
        var versionString = version.ToNormalizedString();

        var package = await PackageDomainService.GetOrCreateAsync(feed, name, versionString);

        var nugetPackage = await _nuGetPackageRepository.FindByNameAsync(feed.Id, name, versionString);

        if (nugetPackage != null)
        {
            throw new NuGetPackageVersionExistsException();
        }

        await _nuGetPackageRepository.InsertAsync(nugetPackage);
    }

    public Task PublishSymbolAsync(string feedName, NuGetV2PackagePublishRequestDto input)
    {
        throw new NotImplementedException();

        //var feed = await GetNuGetFeedByNameAsync(feedName);

        //if (input.Package == null)
        //{
        //    throw new AbpValidationException();
        //}

        //var fileStream = input.Package?.GetStream();

        //if (fileStream == null)
        //{
        //    throw new AbpValidationException();
        //}

        //fileStream.Seek(0, SeekOrigin.Begin);

        //using PackageArchiveReader reader = new PackageArchiveReader(fileStream);
        //// TODO Validate
        //NuspecReader nuspec = reader.NuspecReader;

        //string name = nuspec.GetId();
        //var version = nuspec.GetVersion().ToNormalizedString();

        //var package = await PackageDomainService.GetOrCreateAsync(feed, name, version);

        //var nugetPackage = await _nuGetPackageRepository.FindByNameAsync(feed.Id, name, version);

        //if (nugetPackage != null)
        //{
        //    throw new NuGetPackageVersionExistsException();
        //}

        //nugetPackage = await _nuGetPackageManager.CreatePackageVersionAsync(feed, package, fileStream);

        //nugetPackage.IsSymbolPackage = true;

        //await _nuGetPackageRepository.InsertAsync(nugetPackage);
    }

    public async Task DeleteAsync(string feedName, string id, string version)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        var package = await PackageDomainService.GetAsync(feed, id);

        var nugetPackage = await _nuGetPackageRepository.GetByNameAsync(feed.Id, id, version);

        await _nuGetPackageRepository.DeleteAsync(x => x.PackageId == package.Id && x.Version == version);
    }

    public async Task SetListAsync(string feedName, string id, string version)
    {
        var feed = await GetNuGetFeedByNameAsync(feedName);

        var package = await PackageDomainService.GetAsync(feed, id);

        var nugetPackage = await _nuGetPackageRepository.GetByNameAsync(feed.Id, id, version);

        // TODO
    }
}
