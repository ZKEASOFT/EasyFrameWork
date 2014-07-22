using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Cache;
using Easy.Data;
using Easy.RepositoryPattern;

namespace Easy.Modules.SEO
{
    public class SEOService : ServiceBase<SEOEntity>
    {
        public const string SignalSEOEntityUpdate = "SignalSEOEntityUpdate";
        public override bool Update(SEOEntity item, DataFilter filter)
        {
            new Signal().Do(SignalSEOEntityUpdate);
            return base.Update(item, filter);
        }

        public override bool Update(SEOEntity item, params object[] primaryKeys)
        {
            new Signal().Do(SignalSEOEntityUpdate);
            return base.Update(item, primaryKeys);
        }
    }
}
