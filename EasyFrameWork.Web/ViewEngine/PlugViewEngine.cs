using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easy.Web.ViewEngine
{
    public class PlugViewEngine : RazorViewEngine
    {
        public PlugViewEngine()
        {
            string[] defaultAreaFormat = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" };
            string[] defaultFormat = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
            List<string> areaFormat = new List<string>();
            List<string> normalFormat = new List<string>();

            //init other format in list

            areaFormat.Concat<string>(defaultAreaFormat);
            defaultAreaFormat = areaFormat.ToArray();
            normalFormat.Concat<string>(defaultFormat);
            defaultFormat = normalFormat.ToArray();
            //area
            AreaMasterLocationFormats = defaultAreaFormat;
            AreaPartialViewLocationFormats = defaultAreaFormat;
            AreaViewLocationFormats = defaultAreaFormat;
            //normal
            MasterLocationFormats = defaultFormat;
            PartialViewLocationFormats = defaultFormat;
            ViewLocationFormats = defaultFormat;
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return base.CreatePartialView(controllerContext, partialPath);
        }
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return base.CreateView(controllerContext, viewPath, masterPath);
        }
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, virtualPath);
        }
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
        public override void ReleaseView(ControllerContext controllerContext, IView view)
        {
            base.ReleaseView(controllerContext, view);
        }
    }

}
