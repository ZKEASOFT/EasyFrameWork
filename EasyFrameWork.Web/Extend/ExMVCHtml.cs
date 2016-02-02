using Easy.HTML;
using Easy.Web.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Easy.Web.Extend
{
    public static class ExMVCHtml
    {
        public static MvcHtmlString EditModel<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            using (ViewModelDecode<TModel> de = new ViewModelDecode<TModel>(htmlHelper.ViewData.Model))
            {
                de.ExtendPropertyValue = htmlHelper.ViewContext.Controller.ViewData;
                var tags = de.GetViewModelPropertyHtmlTag();
                List<string> hidenTagsStr = de.GetViewModelHiddenTargets();
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("<div class=\"form-horizontal\">");
                foreach (var item in tags)
                {
                    builder.AppendFormat("<div class='form-group'><label for='{0}' class='col-sm-2 control-label'>{1}</label><div class='col-sm-10'>{2}</div></div>",
                        item.Name, item.DisplayName, item);
                }
                builder.Append("</div>");
                builder.Append("<div id='Hiddens'>");
                foreach (var item in hidenTagsStr)
                {
                    builder.Append(item);
                }
                builder.Append("</div>");
                return new MvcHtmlString(builder.ToString());
            }
        }

        public static MvcHtmlString DisplayModel<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            using (ViewModelDecode<TModel> de = new ViewModelDecode<TModel>(htmlHelper.ViewData.Model))
            {
                var tags = de.GetViewModelPropertyHtmlTag();
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("<div class=\"form-horizontal\">");
                foreach (var item in tags)
                {
                    builder.AppendFormat("<div class='form-group'><label for='{0}' class='col-sm-2 control-label'>{1}</label><div class='col-sm-10'>{2}</div></div>",
                        item.Name, item.DisplayName, item.Value);
                }
                builder.Append("</div>");
                return new MvcHtmlString(builder.ToString());
            }
        }

        public static Grid<T> Grid<T>(this HtmlHelper htmlHelper) where T : class
        {
            return new Grid<T>(htmlHelper.ViewContext);
        }

        public static Grid<TModel> Grid<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : class
        {
            return new Grid<TModel>(htmlHelper.ViewContext);
        }

        public static Tree<T> Tree<T>(this HtmlHelper htmlHelper) where T : class
        {
            return new Tree<T>(htmlHelper.ViewContext);
        }

        public static Tree<TModel> Tree<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : class
        {
            return new Tree<TModel>(htmlHelper.ViewContext);
        }
    }
}
