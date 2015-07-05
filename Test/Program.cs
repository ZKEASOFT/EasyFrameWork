using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
            Permission p = Permission.View | Permission.Create;
            //p 包含 View 也包含 Create 以下验证
            if (p.HasFlag(Permission.View))
            {
                Console.WriteLine("It has View");
            }
            if (p.HasFlag(Permission.Create))
            {
                Console.WriteLine("It has Create");
            }
        }

    }

    [Flags]
    public enum Permission
    {
        View = 1,
        Create = 2,
        Edit = 4,
        Delete = 8
    }
}
