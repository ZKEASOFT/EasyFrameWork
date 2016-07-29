using System.Net;
using System.Web.Mvc;
namespace Easy.Web.HttpActionResult
{
    public class HttpForbiddenResult : HttpStatusCodeResult
    {
        public HttpForbiddenResult()
            : base(HttpStatusCode.Forbidden, null)
        {
        }
    }
}