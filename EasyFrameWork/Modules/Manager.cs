using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules
{
    public static class Manager
    {
        /// <summary>
        /// 注册所有模块;数据字典,多语言,用户
        /// </summary>
        public static void RegistAll()
        {
            RegistDataDictionary();
            RegistMutiLanguage();
            RegistUser();
        }
        /// <summary>
        /// 注册数据字典
        /// </summary>
        public static void RegistDataDictionary()
        {
            new DataDictionary.DataDicttionaryModule().Load();
        }
        /// <summary>
        /// 注册多语言
        /// </summary>
        public static void RegistMutiLanguage()
        {
            new MutiLanguage.MutiLanguageModule().Load();
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        public static void RegistUser()
        {
            new User.UserModule().Load();
        }
    }
}
