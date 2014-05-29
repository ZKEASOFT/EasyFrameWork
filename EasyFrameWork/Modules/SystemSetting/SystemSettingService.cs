using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.SystemSetting
{
    public abstract class SystemSettingService
    {
        SystemSettingRepository rep = new SystemSettingRepository();
        Easy.Cache.StaticCache<SystemSettingBase> cache = new Cache.StaticCache<SystemSettingBase>();
        public virtual SystemSettingBase Get()
        {
            SystemSettingBase setting = cache.Get();
            if (setting == null)
            {
                setting = rep.Get();
                cache.Add(setting);
            }
            return setting;
        }
        public virtual void Update(SystemSettingBase setting)
        {
            rep.Update(setting);
            cache.Add(setting);
        }
    }
}
