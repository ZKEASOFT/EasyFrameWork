using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Extend;

namespace Easy.CMS.Page
{
    public class PageService : ServiceBase<PageEntity>
    {
        public override int Delete(Data.DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any() && this.Get(new Data.DataFilter().Where("ParentId", Constant.OperatorType.In, deletes)).Any())
            {
                this.Delete(new Data.DataFilter().Where("ParentId", Constant.OperatorType.In, deletes));
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            PageEntity page = Get(primaryKeys);
            this.Delete(new Data.DataFilter().Where("ParentId", Constant.OperatorType.Equal, page.ID));
            return base.Delete(primaryKeys);
        }
    }
}
