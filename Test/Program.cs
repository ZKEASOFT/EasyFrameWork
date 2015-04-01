using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
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
            new EmailSender().Send(new EmailContent());
            Console.ReadKey();
        }
    }


    public class EmailContent : EmailContentBase
    {
        public override string GetSubject()
        {
            return "throw new NotImplementedException();";
        }

        public override string GetBody()
        {
            return "throw new NotImplementedException();";
        }


        public override IEnumerable<MailAddress> GetReceivers()
        {
            return new List<MailAddress> { new MailAddress("wayne_wei@keyoutech.com", "SeriaWei") };
        }

        public override MailAddress GetSender()
        {
            return new MailAddress("411367956@qq.com", "Wayne");
        }

        public override string GetSmtpHost()
        {
            return "smtp.qq.com";
        }

        public override NetworkCredential GetCredential()
        {
            return new NetworkCredential("411367956", "wkrlbh1314");
        }
    }


}
