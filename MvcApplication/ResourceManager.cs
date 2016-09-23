/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.Resource;

namespace MvcApplication
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        protected override void InitScript(Func<string, ResourceHelper> script)
        {
            script("base")
                .Include("~/JavaScripts/EasyPlug/jquery.js", "~/JavaScripts/EasyPlug/jquery.js", "//cdn.bootcss.com/jquery/2.2.3/jquery.js")
                .Include("~/JavaScripts/EasyPlug/Easy.js")
                .Include("~/JavaScripts/EasyPlug/Easy.Grid.js")
                .RequiredAtHead();
        }

        protected override void InitStyle(Func<string, ResourceHelper> style)
        {
            style("base")
                .Include("~/Content/Site.css")
                .Include("~/JavaScripts/EasyPlug/Css/Easy.css")
                .Include("~/JavaScripts/EasyPlug/Css/Easy.Grid.css")
                .RequiredAtHead();
        }
    }
}