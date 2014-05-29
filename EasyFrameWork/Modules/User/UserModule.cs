using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.IOCAdapter;
using Easy.Modules.User.Service;

namespace Easy.Modules.User
{
    public class UserModule : IModule
    {
        public void Load()
        {
            Container.Register(typeof(IUserService), typeof(UserService));
        }
    }
}
