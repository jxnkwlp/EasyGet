using System.Text.Json.Serialization;

namespace Passingwind.Protocol.NuGet.Models;

public class RegistrationPackageDependency : PackageDependency
{
    [JsonPropertyName("registration")]
    public string? Registration { get; set; }
}
