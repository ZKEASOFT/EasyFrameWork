using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Easy.Net
{
    public class Ftp
    {
        FtpWebRequest _Ftp;
        string _ftpUrl;
        string _userName;
        string _passWord;
        public Ftp(string ftpUrl, string userName, string passWord)
        {
            this._ftpUrl = ftpUrl;
            this._userName = userName;
            this._passWord = passWord;
            Connect();
        }
        void Connect()
        {
            _Ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(this._ftpUrl));
            _Ftp.UseBinary = true;
            _Ftp.Credentials = new NetworkCredential(this._userName, this._passWord);
        }
        public bool UpLoad(string file)
        {
            FtpWebRequest Ftp = null;
            FileInfo copyfile = new FileInfo(file);
            Ftp.Method = WebRequestMethods.Ftp.UploadFile;
            int bufflength = 2048;
            byte[] buff = new byte[bufflength];
            FileStream Upstream = copyfile.OpenRead();
            int FileOnce;
            try
            {
                Stream Ftpstream = Ftp.GetRequestStream();
                while ((FileOnce = Upstream.Read(buff, 0, bufflength)) > 0)
                {
                    Ftpstream.Write(buff, 0, FileOnce);
                }
                Upstream.Close();
                Ftpstream.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
