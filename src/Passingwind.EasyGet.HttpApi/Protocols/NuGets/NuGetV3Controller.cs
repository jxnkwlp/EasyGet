using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Passingwind.EasyGet.Protocols.NuGets;

[Route("f/nuget/{feedName}/v3")]
public class NuGetV3Controller : EasyGetController, INuGetV3AppService
{
    private readonly INuGetV3AppService _service;

    public NuGetV3Controller(INuGetV3AppService service)
    {
        _service = service;
    }

    [HttpGet("index.json")]
    public Task<NuGetV3ServiceIndexDto> GetAsync(string feedName)
    {
        return _service.GetAsync(feedName);
    }

    [HttpGet("flatcontainer/{id}/{version}/{idVersion}.nuspec")]
    public Task<string> GetPackageSpecAsync(string feedName, string id, string version)
    {
        return _service.GetPackageSpecAsync(feedName, id, version);
    }

    [HttpGet("flatcontainer/{id}/{version}/{idVersion}.nupkg")]
    public Task<IRemoteStreamContent> GetPackageStreamAsync(string feedName, string id, string version)
    {
        return _service.GetPackageStreamAsync(feedName, id, version);
    }

    [HttpGet("registrations/{id}/index.json")]
    public Task<NuGetV3RegistrationIndexResultDto> GetRegistrationAsync(string feedName, string id)
    {
        return _service.GetRegistrationAsync(feedName, id);
    }

    [HttpGet("registrations/{id}/{version}.json")]
    public Task<NuGetV3PackageRegistrationResultDto> GetRegistrationByVersionAsync(string feedName, string id, string version)
    {
        return _service.GetRegistrationByVersionAsync(feedName, id, version);
    }

    [HttpGet("flatcontainer/{id}/index.json")]
    public Task<NuGetV3VersionResultDto> GetVersionsAsync(string feedName, string id)
    {
        return _service.GetVersionsAsync(feedName, id);
    }

    [HttpGet("autocomplete")]
    public Task<NuGetV3SearchAutoCompleteResultDto> SearchAutoCompleteAsync(string feedName, NuGetV3SearchAutoCompleteRequestDto input)
    {
        return _service.SearchAutoCompleteAsync(feedName, input);
    }

    [HttpGet("query")]
    public Task<NuGetV3SearchResultDto> SearchQueryAsync(string feedName, NuGetV3SearchRequestDto input)
    {
        return _service.SearchQueryAsync(feedName, input);
    }
}
