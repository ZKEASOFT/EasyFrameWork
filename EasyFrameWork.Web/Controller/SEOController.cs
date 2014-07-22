using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Easy.Web.Attribute;
using Easy.Modules.SEO;

namespace Easy.Web.Controller
{

    public class SEOController : System.Web.Mvc.Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor is ReflectedActionDescriptor)
            {
                ReflectedActionDescriptor actionDescriptor = filterContext.ActionDescriptor as ReflectedActionDescriptor;
                if (typeof(ActionResult).IsAssignableFrom(actionDescriptor.MethodInfo.ReturnType) && filterContext.ActionDescriptor.GetCustomAttributes(typeof(IgnoreSEOAttribute), true).Length == 0)
                {
                    SEOAttribute seoAttr = null;
                    object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(SEOAttribute), true);
                    if (attributes.Length > 0)
                    {
                        seoAttr = attributes[0] as SEOAttribute;
                    }
                    if (seoAttr == null)
                    {
                        object[] clAttribute = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(SEOAttribute), true);
                        if (clAttribute.Length > 0)
                        {
                            seoAttr = clAttribute[0] as SEOAttribute;
                        }
                    }
                    if (seoAttr != null)
                    {
                        string path = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;
                        path = path.ToLower();
                        string url = "~" + this.HttpContext.Request.RawUrl.ToLower();
                        if (this.HttpContext.Request.ApplicationPath != "/")
                        {
                            url = url.Replace(this.HttpContext.Request.ApplicationPath.ToLower(), "");
                        }
                        url = url.ToLower();
                        if (seoAttr.QueryString != null)
                        {
                            foreach (var item in seoAttr.QueryString)
                            {
                                string qu = this.HttpContext.Request.QueryString[item];
                                if (qu != null)
                                {
                                    path += string.Format("_{0}_{1}", item.ToLower(), qu.ToLower());
                                }
                                else if (filterContext.RequestContext.RouteData.Values.ContainsKey(item))
                                {
                                    qu = filterContext.RequestContext.RouteData.Values[item].ToString();
                                    if (qu != null)
                                    {
                                        path += string.Format("_{0}_{1}", item.ToLower(), qu.ToLower());
                                    }
                                }
                            }
                        }
                        var cache = new Easy.Cache.StaticCache();
                        SEOEntity entity = cache.Get(path, m =>
                        {
                            m.When(SEOService.SignalSEOEntityUpdate);
                            return new SEOService().Get(m.CacheKey);
                        });
                        if (entity != null)
                        {
                            ViewBag.SEO = entity;
                        }
                        else
                        {
                            entity = new SEOEntity
                            {
                                ID = path,
                                Url = url,
                                Title = "未定义",
                                SEOTitle = "zKea[品质·生活]-追求品质人生",
                                SEODescription = "zKea的理念是品质与生活，在这个越来越追求生活的时代，我们一直在努力提升自己的品质。zKea将在各方面助您打造品质人生。",
                                SEOKeyWords = "zkea,品质,生活,交流,见闻,资讯,优铺,学识,优品,冏事",
                                IsPassed = true
                            };
                            // seoService.Add(entity);
                            ViewBag.SEO = entity;
                        }
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                ViewResult result = filterContext.Result as ViewResult;
                if (result.Model != null)
                {
                    Type modelType = result.Model.GetType();
                    Type seoType = typeof(ISEOEntity);
                    if (seoType.IsAssignableFrom(modelType))
                    {
                        ViewBag.SEO = result.Model;
                    }
                    else
                    {
                        PropertyInfo[] propertys = modelType.GetProperties();
                        foreach (PropertyInfo item in propertys)
                        {
                            if (seoType.IsAssignableFrom(item.PropertyType))
                            {
                                object seo = item.GetValue(result.Model, null);
                                if (seo != null)
                                {
                                    ViewBag.SEO = seo;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}