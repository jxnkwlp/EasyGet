using Passingwind.EasyGet.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Passingwind.EasyGet;

/* Inherit your controllers from this class.
 */
public abstract class EasyGetController : AbpControllerBase
{
    protected EasyGetController()
    {
        LocalizationResource = typeof(EasyGetResource);
    }
}
