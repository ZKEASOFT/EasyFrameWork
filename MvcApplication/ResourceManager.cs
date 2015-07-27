using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        public override void InitScript()
        {
            Script("base")
                .Include("~/JavaScripts/EasyPlug/jquery.js")
                .Include("~/JavaScripts/EasyPlug/Easy.js")
                .Include("~/JavaScripts/EasyPlug/Easy.Grid.js")
                .RequiredAtHead();
        }

        public override void InitStyle()
        {
            Style("base")
                .Include("~/Content/Site.css")
                .Include("~/JavaScripts/EasyPlug/Css/Easy.css")
                .Include("~/JavaScripts/EasyPlug/Css/Easy.Grid.css")
                .RequiredAtHead();
        }
    }
}