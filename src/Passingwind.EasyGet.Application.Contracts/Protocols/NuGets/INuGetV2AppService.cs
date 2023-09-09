using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Passingwind.EasyGet.Protocols.NuGets;

public interface INuGetV2AppService : IApplicationService
{
    Task PublishAsync(string feedName, NuGetV2PackagePublishRequestDto input);
    Task PublishSymbolAsync(string feedName, NuGetV2PackagePublishRequestDto input);
    Task DeleteAsync(string feedName, string id, string version);
    Task SetListAsync(string feedName, string id, string version);
}
