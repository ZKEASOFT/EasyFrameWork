using System.Web;
using Easy.IOC.Unity;
using System.Collections;
namespace Easy.Web.Application
{
    public class HttpItemsValueProvider : IHttpItemsValueProvider
    {

        public IDictionary Items
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items;
                }
                return null;
            }
        }
    }
}