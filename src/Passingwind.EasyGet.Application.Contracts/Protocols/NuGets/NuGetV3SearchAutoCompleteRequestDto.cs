namespace Passingwind.EasyGet.Protocols.NuGets;

public class NuGetV3SearchAutoCompleteRequestDto
{
    public string Q { get; set; }

    public string Id { get; set; }

    public bool? Prerelease { get; set; }
    public string PackageType { get; set; }
    public string SemVerLevel { get; set; }

    public int Skip { get; set; }
    public int Take { get; set; }
}
