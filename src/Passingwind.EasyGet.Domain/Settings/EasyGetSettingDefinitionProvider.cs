using Volo.Abp.Settings;

namespace Passingwind.EasyGet.Settings;

public class EasyGetSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EasyGetSettings.MySetting1));
    }
}
