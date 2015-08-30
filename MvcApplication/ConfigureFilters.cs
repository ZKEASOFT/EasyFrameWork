using System.Web.Mvc;
using Easy.Web.Filter;
using MvcApplication.Controllers;

namespace MvcApplication
{
    public class ConfigureFilters : ConfigureFilterBase
    {
        public ConfigureFilters(IFilterRegister register) : base(register)
        {
        }

        public override void Configure()
        {
           
        }
    }

}