using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Web;
using Newtonsoft.Json;

namespace Easy.Storage.QCloud.Common
{
    enum HttpMethod { Get, Post};
    /// <summary>
    /// 请求调用类
    /// </summary>
    class Request
    {
        HttpWebRequest request;
        public string SendRequest(string url, Dictionary<string, string> data, HttpMethod requestMethod,
            Dictionary<string, string> header, int timeOut, string localPath = null, long offset = -1, int sliceSize = 0)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                if (requestMethod == HttpMethod.Get)
                {
                    var paramStr = "";
                    foreach (var key in data.Keys)
                    {
                        paramStr += string.Format("{0}={1}&", key, HttpUtility.UrlEncode(data[key].ToString()));
                    }
                    paramStr = paramStr.TrimEnd('&');
                    url += (url.EndsWith("?") ? "&" : "?") + paramStr;
                }

                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Accept = "*/*";
                request.KeepAlive = true;
                request.UserAgent = "qcloud-dotnet-sdk";
                request.Timeout = timeOut;
                foreach (var key in header.Keys)
                {
                    if(key == "Content-Type")
                    {
                        request.ContentType = header[key];
                    }
                    else
                    {
                        request.Headers.Add(key, header[key]);
                    }
                }
                if (requestMethod == HttpMethod.Post)
                {
                    request.Method = requestMethod.ToString().ToUpper();
                    var memStream = new MemoryStream();
                    if (header.ContainsKey("Content-Type") && header["Content-Type"] == "application/json")
                    {
                        var json = JsonConvert.SerializeObject(data);
                        var jsonByte = Encoding.GetEncoding("utf-8").GetBytes(json.ToString());
                        memStream.Write(jsonByte, 0, jsonByte.Length);
                    }
                    else
                    {
                        var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                        var beginBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                        var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                        request.ContentType = "multipart/form-data; boundary=" + boundary;                       

                        var strBuf = new StringBuilder();
                        foreach (var key in data.Keys)
                        {
                            strBuf.Append("\r\n--" + boundary + "\r\n");
                            strBuf.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n\r\n");
                            strBuf.Append(data[key].ToString());
                        }
                        var paramsByte = Encoding.GetEncoding("utf-8").GetBytes(strBuf.ToString());
                        memStream.Write(paramsByte, 0, paramsByte.Length);

                        if (localPath != null)
                        {
                            memStream.Write(beginBoundary, 0, beginBoundary.Length);
                            var fileInfo = new FileInfo(localPath);
                            var fileStream = new FileStream(localPath, FileMode.Open, FileAccess.Read);

                            const string filePartHeader =
                                "Content-Disposition: form-data; name=\"fileContent\"; filename=\"{0}\"\r\n" +
                                "Content-Type: application/octet-stream\r\n\r\n";
                            var headerText = string.Format(filePartHeader, fileInfo.Name);
                            var headerbytes = Encoding.UTF8.GetBytes(headerText);
                            memStream.Write(headerbytes, 0, headerbytes.Length);

                            if (offset == -1)
                            {
                                var buffer = new byte[1024];
                                int bytesRead;
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    memStream.Write(buffer, 0, bytesRead);
                                }
                            }
                            else
                            {
                                var buffer = new byte[sliceSize];
                                int bytesRead;
                                fileStream.Seek(offset, SeekOrigin.Begin);
                                bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                                memStream.Write(buffer, 0, bytesRead);
                            }
                        }
                        memStream.Write(endBoundary, 0, endBoundary.Length);
                    }
                    request.ContentLength = memStream.Length;
                    var requestStream = request.GetRequestStream();
                    memStream.Position = 0;
                    var tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();
                }
                var response = request.GetResponse();
                using (var s = response.GetResponseStream())
                {
                    var reader = new StreamReader(s, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var s = we.Response.GetResponseStream())
                    {
                        var reader = new StreamReader(s, Encoding.UTF8);
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    throw we;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
