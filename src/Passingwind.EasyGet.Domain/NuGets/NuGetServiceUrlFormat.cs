namespace Passingwind.EasyGet.NuGets;

public static class NuGetServiceUrlFormat
{
    public const string SearchBaseUrl = "{0}/v3/query";
    public const string AutocompleteBaseUrl = "{0}/v3/autocomplete";
    public const string PackageBaseUrl = "{0}/v3/flatcontainer";
    public const string RegistrationsBaseUrl = "{0}/v3/registrations";
    public const string CatalogBaseUrl = "{0}/v3/catalog";

    public const string PackageUrl = "{0}/v3/flatcontainer/{1}/{2}.json";
    public const string PackageContentUrl = "{0}/v3/flatcontainer/{1}/{2}/{1}.{2}.nupkg";
    public const string RegistrationIndexUrl = "{0}/v3/registrations/{1}.json";
    public const string RegistrationUrl = "{0}/v3/registrations/{1}/{2}.json";
    public const string CatalogIndexUrl = "{0}/v3/catalog/{1}.json";
    public const string CatalogUrl = "{0}/v3/catalog/{1}/{2}.json";

    public const string PackagePublishBaseUrl = "{0}/v2/package";
    public const string SymbolPackagePublishBaseUrl = "{0}/v2/symbolpackage";

    public const string PackageViewUrl = "{0}/package/{1}/{2}";
}
