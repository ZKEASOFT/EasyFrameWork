using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public static class SystemSetting
    {
        static Modules.SystemSetting.SystemSettingService service;
        static SystemSetting()
        {
            service = Loader.CreateInstance<Modules.SystemSetting.SystemSettingService>();
        }
        public static Modules.SystemSetting.SystemSettingBase Get()
        {
            return service.Get();
        }
        public static T Get<T>() where T : Modules.SystemSetting.SystemSettingBase
        {
            return service.Get() as T;
        }
        public static void Update(Modules.SystemSetting.SystemSettingBase setting)
        {
            service.Update(setting);
        }
    }
}
