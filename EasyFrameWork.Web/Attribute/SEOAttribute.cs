using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.Attribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class SEOAttribute : System.Attribute
    {
        public List<string> QueryString;
        public SEOAttribute(params string[] query)
        {
            if (query != null && query.Length > 0)
            {
                QueryString = new List<string>();
                foreach (var item in query)
                {
                    QueryString.Add(item);
                }
            }
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnoreSEOAttribute : System.Attribute
    {

    }
}
