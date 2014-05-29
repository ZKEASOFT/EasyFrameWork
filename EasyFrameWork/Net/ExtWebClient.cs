using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Easy.Net
{
    public class ExtWebClient : WebClient
    {
        CookieContainer cookieContainer;
        public ExtWebClient()
        {
            this.cookieContainer = new CookieContainer();
        }
        public CookieContainer Cookies
        {
            get { return this.cookieContainer; }
            set { this.cookieContainer = value; }
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = cookieContainer;
            }
            return request;
        }
    }
}
