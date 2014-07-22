using Easy.IOCAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.MutiLanguage
{
    public class MutiLanguageModule : IModule
    {
        public void Load()
        {
            Container.Register(typeof(ILanguageService), typeof(LanguageService));
        }
    }
}
