using Easy.Models;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Security
{
    public class DefaultAuthorizer : IAuthorizer
    {

        public bool Authorize(int permission)
        {
            return Authorize(permission, ServiceLocator.Current.GetInstance<IApplicationContext>().CurrentUser);
        }

        public bool Authorize(int permission, IUser user)
        {
            if (user.PermissionValue == 0)
            {
                user.PermissionValue = ServiceLocator.Current.GetInstance<IUserService>().GetPermissionValue(user.UserID);
                if (user.PermissionValue < 0) return false;
            }

            return (user.PermissionValue & permission) == permission;
        }
    }
}