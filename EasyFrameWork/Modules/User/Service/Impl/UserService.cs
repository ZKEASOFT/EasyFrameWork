using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Modules.User.Models;

namespace Easy.Modules.User.Service
{
    public class UserService : ServiceBase<UserEntity>, IUserService
    {
        public UserService()
        {

        }

        public UserEntity Login(string userID, string passWord)
        {
            passWord = Easy.EncryptionTool.Encryption(passWord);
            var result = this.Get(new Data.DataFilter().Where("UserID", Easy.Constant.DataEnumerate.OperatorType.Equal, userID)
                .Where("PassWord", Easy.Constant.DataEnumerate.OperatorType.Equal, passWord).Where("IsPassed=true"));
            if (result.Count == 1)
            {
                var user = result.First();
                user.LastLoginDate = DateTime.Now;
                this.Update(user);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
