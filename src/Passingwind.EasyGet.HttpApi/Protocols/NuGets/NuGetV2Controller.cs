using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Passingwind.EasyGet.Protocols.NuGets;

[Route("f/nuget/{feedName}/v2")]
public class NuGetV2Controller : EasyGetController, INuGetV2AppService
{
    private readonly INuGetV2AppService _service;

    public NuGetV2Controller(INuGetV2AppService service)
    {
        _service = service;
    }

    [HttpDelete("package/{id}/{version}")]
    public Task DeleteAsync(string feedName, string id, string version)
    {
        return _service.DeleteAsync(feedName, id, version);
    }

    [HttpPut("package")]
    public Task PublishAsync(string feedName, NuGetV2PackagePublishRequestDto input)
    {
        return _service.PublishAsync(feedName, input);
    }

    [HttpPut("symbolpackage")]
    public Task PublishSymbolAsync(string feedName, NuGetV2PackagePublishRequestDto input)
    {
        return _service.PublishSymbolAsync(feedName, input);
    }

    [HttpPost("package/{id}/{version}")]
    public Task SetListAsync(string feedName, string id, string version)
    {
        return _service.SetListAsync(feedName, id, version);
    }
}
