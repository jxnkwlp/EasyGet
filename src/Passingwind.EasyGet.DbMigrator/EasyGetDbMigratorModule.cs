using Passingwind.EasyGet.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Passingwind.EasyGet.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EasyGetEntityFrameworkCoreModule),
    typeof(EasyGetApplicationContractsModule)
    )]
public class EasyGetDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
