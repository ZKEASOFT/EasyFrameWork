using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Easy.Storage.QCloud
{
    public class Configuration: ConfigurationSection
    {

        public static Configuration GetConfiguration()
        {
            Configuration configuration = ConfigurationManager.GetSection("QCloudStorage") as Configuration;
            if (configuration != null)
                return configuration;
            return new Configuration();
        }

        [ConfigurationProperty("bucketName")]
        public string BucketName
        {
            get
            {
                return (string)this["bucketName"];
            }
            set
            {
                this["bucketName"] = value;
            }
        }

        [ConfigurationProperty("appId")]
        public int AppID
        {
            get
            {
                return Convert.ToInt32(this["appId"]);
            }
            set
            {
                this["appId"] = value;
            }
        }
        [ConfigurationProperty("secretId")]
        public string SecretId
        {
            get
            {
                return (string)this["secretId"];
            }
            set
            {
                this["secretId"] = value;
            }
        }
        [ConfigurationProperty("secretKey")]
        public string SecretKey
        {
            get
            {
                return (string)this["secretKey"];
            }
            set
            {
                this["secretKey"] = value;
            }
        }
    }
}
