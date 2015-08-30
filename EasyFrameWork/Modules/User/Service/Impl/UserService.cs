using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.Modules.User.Models;
using Easy.Constant;

namespace Easy.Modules.User.Service
{
    public class UserService : ServiceBase<UserEntity>, IUserService
    {
        public override void Add(UserEntity item)
        {
            item.PassWord = EncryptionTool.Encryption(item.PassWord);
            base.Add(item);
        }

        public UserEntity Login(string userID, string passWord)
        {
            passWord = EncryptionTool.Encryption(passWord);
            var result = Get(new DataFilter().Where("UserID", OperatorType.Equal, userID)
                .Where("PassWord", OperatorType.Equal, passWord));
            if (result.Any())
            {
                var user = result.First();
                user.LastLoginDate = DateTime.Now;
                Update(user);
                return user;
            }
            return null;
        }
    }
}
