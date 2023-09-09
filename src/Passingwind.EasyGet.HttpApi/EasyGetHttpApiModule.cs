using Localization.Resources.AbpUi;
using Passingwind.EasyGet.Localization;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Passingwind.EasyGet;

[DependsOn(
    typeof(EasyGetApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule)
    )]
public class EasyGetHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<EasyGetResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
