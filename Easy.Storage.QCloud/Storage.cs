using Easy.Storage.QCloud.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Extend;
using Newtonsoft.Json.Linq;

namespace Easy.Storage.QCloud
{
    public class Storage : IStorageService
    {
        private readonly CosCloud _client;
        private string bucketName;
        public Storage()
        {
            var config = Configuration.GetConfiguration();
            bucketName = config.BucketName;
            _client = new CosCloud(config.AppID, config.SecretId, config.SecretKey);
        }
        string RelativePath(string path)
        {
            if (path.IsNotNullAndWhiteSpace())
            {
                path = path.Replace(AppDomain.CurrentDomain.BaseDirectory, "/").Replace("\\", "/");
            }
            return path;
        }
        public string CreateFolder(string folder)
        {
            var result = _client.CreateFolder(bucketName, RelativePath(folder));
            if (result.IsNotNullAndWhiteSpace() && result.Contains("resource_path"))
            {
                JToken token = JObject.Parse(result);
                var data = token.SelectToken("data");
                return (string)data.SelectToken("resource_path");
            }
            return null;
        }

        public string DeleteFile(string file)
        {
            return _client.DeleteFile(bucketName, RelativePath(file));
        }

        public string DeleteFolder(string folder)
        {
            return _client.DeleteFolder(bucketName, RelativePath(folder));
        }

        public string SaveFile(string file)
        {
            string result = _client.UploadFile(bucketName, RelativePath(file), file);
            if (result.IsNotNullAndWhiteSpace() && result.Contains("access_url"))
            {
                JToken token = JObject.Parse(result);
                var data = token.SelectToken("data");
                return (string)data.SelectToken("access_url");
            }
            return null;
        }
    }
}
