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
        public static MvcHtmlString EasyTagFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            // ModelMetadata meta = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            ViewModelDecode<TModel> de = new ViewModelDecode<TModel>(htmlHelper.ViewData.Model);
            string name = ExpressionHelper.GetExpressionText(expression);
            return new MvcHtmlString(de.GetViewModelPropertyHtmlTag(name).ToString());
        }
        public static MvcHtmlString EasyLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ViewModelDecode<TModel> de = new ViewModelDecode<TModel>(htmlHelper.ViewData.Model);
            string name = ExpressionHelper.GetExpressionText(expression);
            return new MvcHtmlString(string.Format("<label for='{0}'>{1}</label>", name, de.GetPropertyDisplayName(name)));
        }
        public static MvcHtmlString EasyTags<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            ViewModelDecode<TModel> de = new ViewModelDecode<TModel>(htmlHelper.ViewData.Model);
            List<string> tagsStr = de.GetViewModelPropertyHtmlTag(true);
            List<string> hidenTagsStr = de.GetViewModelHiddenTargets();

            StringBuilder builder = new StringBuilder();
            foreach (var item in tagsStr)
            {
                builder.AppendFormat("<div class='EasyModelTag'><div class='TagCol'>{0}</div></div>", item);
            }
            builder.Append("<div id='Hiddens'>");
            foreach (var item in hidenTagsStr)
            {
                builder.Append(item);
            }
            builder.Append("</div> <div style='clear:both'></div>");
            return new MvcHtmlString(builder.ToString());
        }
        public static MvcHtmlString EasyTags<TModel>(this HtmlHelper<TModel> htmlHelper, int cols)
        {
            ViewModelDecode<TModel> de = new ViewModelDecode<TModel>(htmlHelper.ViewData.Model);
            List<string> tagsStr = de.GetViewModelPropertyHtmlTag(true);
            List<string> hidenTagsStr = de.GetViewModelHiddenTargets();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < tagsStr.Count; i++)
            {
                if (i % cols == 0)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append("<div style='clear:both'></div></div>");
                    }
                    builder.Append("<div class='EasyModelTag' style='width:100%'>");
                }
                builder.AppendFormat("<div class='TagCol' style='width:{0}%;'><div>{1}</div></div>", 100 / cols, tagsStr[i]);
            }
            builder.Append("<div style='clear:both'></div></div>");
            builder.Append("<div id='Hiddens'>");
            foreach (var item in hidenTagsStr)
            {
                builder.Append(item);
            }
            builder.Append("</div> <div style='clear:both'></div>");
            return new MvcHtmlString(builder.ToString());
        }
        public static MvcHtmlString EasyTagsForDisplay<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            ViewModelDecode<TModel> de = new ViewModelDecode<TModel>(htmlHelper.ViewData.Model);
            var tags = de.GetViewModelPropertyHtmlTag();
            StringBuilder builder = new StringBuilder();
            foreach (var item in tags)
            {
                builder.AppendFormat("<div class='EasyModelTag'><div class='TagCol'><label>{0}</label><span>{1}</span></div></div>", item.DisplayName, item.Value);
            }
            builder.Append("<div style='clear:both'></div>");
            return new MvcHtmlString(builder.ToString());
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
