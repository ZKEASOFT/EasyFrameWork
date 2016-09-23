/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.Security;
using Easy.Web.Authorize;
using Easy.Web.Filter;
using MvcApplication.Controllers;

namespace MvcApplication
{
    public class ConfigureFilters : ConfigureFilterBase
    {
        public ConfigureFilters(IFilterRegister register)
            : base(register)
        {
           // register.Register<HomeController, DefaultAuthorizeAttribute>(c => c.Index(), ac => ac.SetPermissionKey(""));
        }

        public override void Configure()
        {

        }
    }

}