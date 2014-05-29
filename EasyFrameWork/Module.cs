using Easy.IOCAdapter;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy
{
    public class Module
    {
        static string appPath;
        /// <summary>
        /// 虚拟目录名称
        /// </summary>
        public static string ApplicationName
        {
            get
            {
                if (!string.IsNullOrEmpty(appPath)) return appPath;
                IApplicationContext app = Easy.IOCAdapter.Loader.CreateInstance<IApplicationContext>();
                if (app != null)
                {
                    appPath = app.VirtualPath;
                    return app.VirtualPath;
                }
                else return null;
            }

        }
    }
}
