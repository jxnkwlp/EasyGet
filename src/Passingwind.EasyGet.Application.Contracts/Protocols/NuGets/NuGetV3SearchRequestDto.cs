namespace Passingwind.EasyGet.Protocols.NuGets;

public class NuGetV3SearchRequestDto
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string Q { get; set; }
    public bool? Prerelease { get; set; }
    public string SemVerLevel { get; set; }
    public string PackageType { get; set; }
}
