using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace Easy.Web.HTML
{
    public class Grid<T> : Easy.HTML.Grid.EasyGrid<T> where T : class
    {
        ViewContext viewContex;
        public Grid(ViewContext viewContex)
        {
            this.viewContex = viewContex;
            string controller = viewContex.RouteData.Values["controller"].ToString();
            string area = string.Empty;
            string getList = string.Empty;
            string delete = string.Empty;
            if (viewContex.RouteData.DataTokens.ContainsKey("area"))
            {
                area = viewContex.RouteData.DataTokens["area"].ToString();
            }
            if (string.IsNullOrEmpty(area))
            {
                getList = (this.viewContex.Controller as System.Web.Mvc.Controller).Url.Content(string.Format("~/{0}/GetList", controller));
                delete = (this.viewContex.Controller as System.Web.Mvc.Controller).Url.Content(string.Format("~/{0}/Delete", controller));
            }
            else
            {
                getList = (this.viewContex.Controller as System.Web.Mvc.Controller).Url.Content(string.Format("~/{0}/{1}/GetList", area, controller));
                delete = (this.viewContex.Controller as System.Web.Mvc.Controller).Url.Content(string.Format("~/{0}/{1}/Delete", area, controller));
            }
            base.DataSource(getList);
            base.DeleteUrl(delete);
        }
        public override Easy.HTML.Grid.EasyGrid<T> DataSource(string url)
        {
            return base.DataSource((this.viewContex.Controller as System.Web.Mvc.Controller).Url.Content(url));
        }
        public override Easy.HTML.Grid.EasyGrid<T> DeleteUrl(string url)
        {
            return base.DeleteUrl((this.viewContex.Controller as System.Web.Mvc.Controller).Url.Content(url));
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
