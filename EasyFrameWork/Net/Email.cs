using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Net
{
    public class Email
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="ReEmail">接收人邮件地址</param>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Content">邮件内容（可以是纯文本格式，也可以是HTML格式）</param>
        /// <param name="Html">是否是HTML格式</param>
        /// <param name="SendEmail">发送人的Email地址</param>
        /// <param name="PassWord">发送人的Email的密码</param>
        /// <param name="SendName">发送人姓名</param>
        /// <param name="eHost">邮件服务器地址，例:smtp.qq.com</param>
        /// <returns>返回发送结果</returns>
        public static bool SendEmail(string ReEmail, string Subject, string Content, bool Html, string SendEmail, string PassWord, string SendName, string eHost)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(ReEmail);//接收人
                mail.Subject = Subject;
                mail.Body = Content;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = Html;
                mail.From = new System.Net.Mail.MailAddress(SendEmail, SendName, Encoding.UTF8);//发送人
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = eHost;
                client.Credentials = new System.Net.NetworkCredential(SendEmail.Split('@')[0], PassWord);
                client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
