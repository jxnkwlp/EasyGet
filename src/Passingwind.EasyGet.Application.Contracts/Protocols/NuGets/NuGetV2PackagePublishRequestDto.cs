using Volo.Abp.Content;

namespace Passingwind.EasyGet.Protocols.NuGets;
public class NuGetV2PackagePublishRequestDto
{
    public IRemoteStreamContent Package { get; set; }
}
