using Easy.Cache;
using Easy.IOCAdapter.Exceptions;
using Easy.Modules.MutiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy
{
    public static class Localization
    {
        public static string Get(string lanKey)
        {
            StaticCache<ILanguageEntity> lanCache = new StaticCache<ILanguageEntity>();
            ILanguageEntity lan = lanCache.Get(lanKey);
            if (lan == null)
            {
                ILanguageService lanService;
                try
                {
                    lanService = Easy.IOCAdapter.Loader.CreateInstance<ILanguageService>();
                }
                catch (UnregisteredException ex)
                {
                    return lanKey;
                }
                if (lanService == null)
                    return lanKey;
                lan = lanService.Get(lanKey, GetCurrentLanID());
                if (lan == null)
                {
                    string lanValue = lanKey;
                    string LanType = "UnKnown";
                    string Module = "Unknown";
                    if (lanKey.Contains("@"))
                    {
                        lanValue = lanKey.Split('@')[1];
                        LanType = "EntityProperty";
                        Module = lanKey.Split('@')[0];
                    }
                    lan = new LanguageEntity();
                    lan.LanID = GetCurrentLanID();
                    lan.LanValue = lanValue;
                    lan.LanKey = lanKey;
                    lan.LanType = LanType;
                    lan.Module = Module;
                    lanService.Add(lan);
                    lanCache.Add(lanKey, lan);
                    return lanValue;
                }
                else
                {
                    lanCache.Add(lanKey, lan);
                }
            }
            return lan.LanValue;
        }
        public static Dictionary<string, string> InitLan(Dictionary<string, string> source)
        {
            ILanguageService lanService;
            try
            {
                lanService = Easy.IOCAdapter.Loader.CreateInstance<ILanguageService>();
            }
            catch (UnregisteredException ex)
            {
                foreach (string item in source.Keys.ToArray<string>())
                {
                    source[item] = item;
                }
                return source;
            }
            if (lanService != null)
                return lanService.InitLan(source);
            else
            {
                foreach (string item in source.Keys.ToArray<string>())
                {
                    source[item] = item;
                }
                return source;
            }
        }
        public static int GetCurrentLanID()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
        }
    }
}
