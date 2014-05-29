using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Net
{
    public class Http
    {
        /// <summary>
        /// 获取网对应的html(GET)
        /// </summary>
        /// <param name="Url">网址</param>
        /// <returns>返回HTML</returns>
        public static string GetResponse_Html(string Url)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.Default))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取网对应的html(GET)
        /// </summary>
        /// <param name="Url">网址</param>
        /// <param name="proxy">要使用的代理</param>
        /// <returns>返回HTML</returns>
        public static string GetResponse_Html(string Url, System.Net.WebProxy proxy)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                wReq.Proxy = proxy;
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.Default))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 用POST方法提交数据
        /// </summary>
        /// <param name="URL">提交的地址</param>
        /// <param name="Postdata">提交的数据</param>
        /// <returns>返回HTML</returns>
        public static string PostData(string URL, string Postdata)
        {
            Encoding encoding = Encoding.Default;
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes(Postdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 用POST方法提交数据
        /// </summary>
        /// <param name="URL">提交的地址</param>
        /// <param name="Postdata">提交的数据</param>
        /// <param name="proxy">要使用的代理</param>
        /// <returns>返回HTML</returns>
        public static string PostData(string URL, string Postdata, System.Net.WebProxy proxy)
        {
            Encoding encoding = Encoding.Default;
            System.Net.WebRequest request = System.Net.WebRequest.Create(URL);
            request.Proxy = proxy;
            request.Method = "post";
            //request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes(Postdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            System.Net.WebResponse response = request.GetResponse();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 获取对应IP的IP地址
        /// </summary>
        /// <param name="IPaddress">IP地址</param>
        /// <returns>返回位置信息</returns>
        public static string GetIpAddressFree(string IPaddress)
        {

            System.Net.HttpWebRequest req = System.Net.WebRequest.Create("http://www.youdao.com/smartresult-xml/search.s?type=ip&q=" + IPaddress) as System.Net.HttpWebRequest;
            System.Net.WebResponse response = req.GetResponse();
            string address = string.Empty;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(reader.ReadToEnd());
                address = doc.DocumentElement.ChildNodes[0].ChildNodes[1].InnerText;
            }
            return address;
        }


        /// <summary>
        /// 获取对应手机号的归属地
        /// </summary>
        /// <param name="Phone">手机号码</param>
        /// <returns>归属地</returns>
        public static string GetPhoneLocation(string Phone)
        {

            System.Net.HttpWebRequest req = System.Net.WebRequest.Create("http://www.youdao.com/smartresult-xml/search.s?type=mobile&q=" + Phone) as System.Net.HttpWebRequest;
            System.Net.WebResponse response = req.GetResponse();
            string address = string.Empty;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(reader.ReadToEnd());
                address = doc.DocumentElement.ChildNodes[0].ChildNodes[1].InnerText;
            }
            return address;
        }

        /// <summary>
        /// 获取身份证信息
        /// </summary>
        /// <param name="CardID">身份证号</param>
        /// <returns>身份证地区信息</returns>
        public static string GetUserCard(string CardID)
        {

            System.Net.HttpWebRequest req = System.Net.WebRequest.Create("http://www.youdao.com/smartresult-xml/search.s?type=id&q=" + CardID) as System.Net.HttpWebRequest;
            System.Net.WebResponse response = req.GetResponse();
            string address = string.Empty;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(reader.ReadToEnd());
                address = doc.DocumentElement.ChildNodes[0].ChildNodes[1].InnerText + " 出生日期" + doc.DocumentElement.ChildNodes[0].ChildNodes[2].InnerText;
            }
            return address;
        }
    }
}
