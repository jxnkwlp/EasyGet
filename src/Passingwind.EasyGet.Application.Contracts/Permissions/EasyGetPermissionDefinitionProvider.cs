using Volo.Abp.Authorization.Permissions;

namespace Passingwind.EasyGet.Permissions;

public class EasyGetPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EasyGetPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EasyGetPermissions.MyPermission1, L("Permission:MyPermission1"));
    }
}
