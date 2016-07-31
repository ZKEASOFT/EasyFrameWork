using System.Linq;
using Easy.Data;
using Easy.Extend;
using Easy.Models;
using Easy.Modules.Role;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Security
{
    public class DefaultAuthorizer : IAuthorizer
    {

        public bool Authorize(string permission)
        {
            return Authorize(permission, ServiceLocator.Current.GetInstance<IApplicationContext>().CurrentUser);
        }

        public bool Authorize(string permission, IUser user)
        {
            if (permission.IsNullOrWhiteSpace())
            {
                return true;
            }
            if (user.Roles == null || !user.Roles.Any())
            {
                return false;
            }
            var roles = user.Roles.ToList(m => m.ID);
            return ServiceLocator.Current.GetInstance<IRoleService>()
                 .Get(new DataFilter().Where("ID", OperatorType.In, roles))
                 .Any(r => r.Permissions.Any(p => p.PermissionKey == permission));
        }
    }
}