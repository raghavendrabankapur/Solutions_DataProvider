using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Solutions_DataProvider.DataProvider
{
    public class DataAccess
    {
        string _country = string.Empty;
        JObject obj = null;
        public DataAccess(string environment, string region, string country = null)
        {
            GetFile(environment, region);
            _country = country != null ? country.ToLower() : string.Empty;
        }

        public string GetKey(string key)
        {
            string val = string.Empty;
            if (obj == null)
            {
                throw new Exception("No environment with the region found");
            }
            else
            {
                if (string.IsNullOrEmpty(_country))
                {
                    val = obj.ToString();
                }
                else
                {
                    var root = obj[_country];
                    foreach (var item in key.Split(':'))
                    {
                        root = root[item];
                    }

                    val = root != null ? root.ToString() : throw new Exception($"Cound not find key {key}");
                }
                return val;
            }
        }

        private void GetFile(string environment, string region)
        {
            JObject obj = null;

            string filename = string.Empty;
            bool isWindows = RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
            Console.WriteLine($"Platfomr detected is {RuntimeInformation.OSDescription}");
            if (isWindows)
                filename = $"{Directory.GetCurrentDirectory()}\\DataProvider\\Data\\{environment.ToLower()}\\{region.ToLower()}.json";
            else
                filename = $"{Directory.GetCurrentDirectory()}/DataProvider/Data/{environment.ToLower()}/{region.ToLower()}.json";
            StreamReader stream = File.OpenText(filename);

            JsonTextReader reader = new JsonTextReader(stream);

            obj = (JObject)JToken.ReadFrom(reader);
        }
    }
}
