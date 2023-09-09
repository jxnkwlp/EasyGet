using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Passingwind.EasyGet.Data;

/* This is used if database provider does't define
 * IEasyGetDbSchemaMigrator implementation.
 */
public class NullEasyGetDbSchemaMigrator : IEasyGetDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
