using System.Web;
using Easy.IOC.Unity;
using System.Collections;
namespace Easy.Web.Application
{
    public class HttpItemsValueProvider : IHttpItemsValueProvider
    {

        public IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }
    }
}