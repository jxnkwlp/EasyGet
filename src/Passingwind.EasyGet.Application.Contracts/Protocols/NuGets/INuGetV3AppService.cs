using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Passingwind.EasyGet.Protocols.NuGets;

public interface INuGetV3AppService : IApplicationService
{
    #region index

    Task<NuGetV3ServiceIndexDto> GetAsync(string feedName);

    #endregion index

    #region package base 

    /// <summary>
    ///  https://learn.microsoft.com/nuget/api/package-base-address-resource#enumerate-package-versions
    /// </summary>
    /// <param name="feedName"></param>
    /// <param name="id"></param>
    Task<NuGetV3VersionResultDto> GetVersionsAsync(string feedName, string id);
    /// <summary>
    ///  https://learn.microsoft.com/nuget/api/package-base-address-resource#download-package-content-nupkg
    /// </summary>
    /// <param name="feedName"></param>
    /// <param name="id"></param>
    /// <param name="version"></param>
    Task<IRemoteStreamContent> GetPackageStreamAsync(string feedName, string id, string version);
    /// <summary>
    ///  https://learn.microsoft.com/nuget/api/package-base-address-resource#download-package-manifest-nuspec
    /// </summary>
    /// <param name="feedName"></param>
    /// <param name="id"></param>
    /// <param name="version"></param>
    Task<string> GetPackageSpecAsync(string feedName, string id, string version);

    #endregion package base 

    #region registration 

    /// <summary>
    ///  https://learn.microsoft.com/nuget/api/registration-base-url-resource#registration-index
    /// </summary>
    /// <param name="feedName"></param>
    /// <param name="id"></param>
    Task<NuGetV3RegistrationIndexResultDto> GetRegistrationAsync(string feedName, string id);
    Task<NuGetV3PackageRegistrationResultDto> GetRegistrationByVersionAsync(string feedName, string id, string version);

    #endregion registration 

    #region search-autocomplete 

    /// <summary>
    ///  https://learn.microsoft.com/en-us/nuget/api/search-autocomplete-service-resource
    /// </summary>
    /// <param name="feedName"></param>
    /// <param name="input"></param>
    Task<NuGetV3SearchAutoCompleteResultDto> SearchAutoCompleteAsync(string feedName, NuGetV3SearchAutoCompleteRequestDto input);

    #endregion search-autocomplete 

    #region search 

    /// <summary>
    ///  https://learn.microsoft.com/en-us/nuget/api/search-query-service-resource
    /// </summary>
    /// <param name="feedName"></param>
    /// <param name="input"></param>
    Task<NuGetV3SearchResultDto> SearchQueryAsync(string feedName, NuGetV3SearchRequestDto input);

    #endregion search 

    //#region vulnerability-info#vulnerability-index

    //Task GetVulnerabilityIndexAsync(string feedName);
    //Task GetVulnerabilityBaseAsync(string feedName);
    //Task GetVulnerabilityUpdateAsync(string feedName);

    //#endregion vulnerability-info#vulnerability-index
}
