using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace Easy.Web.HTML
{
    public class Tree<T> : Easy.HTML.zTree.Tree<T> where T : class
    {
        ViewContext viewContex;
        public Tree(ViewContext viewContex)
        {
            this.viewContex = viewContex;
        }
        public override Easy.HTML.zTree.Tree<T> Source(string url)
        {
            return base.Source((viewContex.Controller as System.Web.Mvc.Controller).Url.Content(url));
        }
        public override string ToString()
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(viewContex.Writer))
            {
                writer.Write(base.ToString());
            }
            return null;
        }
        public MvcHtmlString ToHtmlString()
        {
            return new MvcHtmlString(base.ToString());
        }
    }
}
