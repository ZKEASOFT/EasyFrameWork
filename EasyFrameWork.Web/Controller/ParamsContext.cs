using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.Controller
{
    public class ParamsContext<T>
    {
        public T ID { get; set; }
        public T ParentID { get; set; }
    }
}
