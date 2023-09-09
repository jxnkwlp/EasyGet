using System.Threading.Tasks;

namespace Passingwind.EasyGet.Data;

public interface IEasyGetDbSchemaMigrator
{
    Task MigrateAsync();
}
