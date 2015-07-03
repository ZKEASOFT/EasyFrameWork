using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Easy;
using Easy.Extend;
using Easy.IOCAdapter;
using Easy.Net.Email;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string math = "http://media.kingston.com/hyperx/sidekick/sidekick-support-landing_6b7d8f.jpg";

           Regex reg=new Regex(@"[-|_]([a-fA-F0-9]{6})\.(?:jpg|png)$");
            reg.Replace(math, (v) =>
            {
                return "";
            });
           Console.WriteLine(reg.IsMatch(math));
            Console.ReadKey();
        }
    }

}
