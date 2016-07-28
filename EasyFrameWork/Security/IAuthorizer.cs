using Easy.Models;
using Easy.Modules.User.Models;

namespace Easy.Security
{
    public interface IAuthorizer
    {
        bool Authorize(int permission);
        bool Authorize(int permission, IUser user);
    }
}