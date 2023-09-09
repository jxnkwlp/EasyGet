using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Passingwind.EasyGet.Data;
using Volo.Abp.DependencyInjection;

namespace Passingwind.EasyGet.EntityFrameworkCore;

public class EntityFrameworkCoreEasyGetDbSchemaMigrator
    : IEasyGetDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreEasyGetDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the EasyGetDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<EasyGetDbContext>()
            .Database
            .MigrateAsync();
    }
}
