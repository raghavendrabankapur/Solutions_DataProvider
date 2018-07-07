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
        string _country, _environment, _region;
        private JObject obj;
        public DataAccess(string environment, string region, string country = null)
        {
            _environment = environment;
            _region = region;
            _country = country != null ? country.ToLower() : string.Empty;
            GetFile();
        }

        private JObject GetObject(string key, JObject fromObj=null)
        {
            JObject root = null;
            if(fromObj == null)
            {
                fromObj = obj;
            }
            if (fromObj == null)
            {
                throw new Exception("No environment with the region found");
            }
            else
            {
                if (string.IsNullOrEmpty(_country))
                {
                    return fromObj;
                }
                else
                {
                    root = fromObj[_country] as JObject;
                    foreach (var item in key.Split(':'))
                    {
                        if (root[item].GetType().Equals(typeof(JArray)))
                        {
                            root = (root[item])[0].ToObject<JObject>();
                        }
                        else
                        {
                            root = (root[item]).ToObject<JObject>();
                        }
                    }
                }
                return root;
            }
        }

        public string Get(string key)
        {
            string val = string.Empty;
            var root = GetObject(key);
            val = root != null ? root.ToString() : throw new Exception($"Cound not find key {key}");
            return val;
        }

        public string Update(string jsonToBeUpdated)
        {
            string path = GetFilePath();
            lock (new object())
            {
                File.WriteAllText(path, jsonToBeUpdated);
            }
            return "Environment updated";
        }

        public string UpdateKey(string key, string value)
        {
            string json = File.ReadAllText(GetFilePath());
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            var t = ((JObject)jsonObj).Children();

            foreach (var item in t)
            {
            }

            if (!string.IsNullOrEmpty(_country))
            {
                jsonObj = jsonObj[_country];
            }

            foreach (var item in key.Split(':'))
            {
                jsonObj = jsonObj[item];
            }

            jsonObj = "new password";
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(GetFilePath(), output);
            return "Updated the key";
        }

        private string GetFilePath()
        {
            string filename = string.Empty;
            bool isWindows = RuntimeInformation
                                              .IsOSPlatform(OSPlatform.Windows);
            Console.WriteLine($"Platfomr detected is {RuntimeInformation.OSDescription}");
            if (isWindows)
                filename = $"{Directory.GetCurrentDirectory()}\\DataProvider\\Data\\{_environment.ToLower()}\\{_region.ToLower()}.json";
            else
                filename = $"{Directory.GetCurrentDirectory()}/DataProvider/Data/{_environment.ToLower()}/{_region.ToLower()}.json";
            return filename;
        }

        private void GetFile()
        {
            StreamReader stream = File.OpenText(GetFilePath());

            JsonTextReader reader = new JsonTextReader(stream);

            this.obj = (JObject)JToken.ReadFrom(reader);
            reader.Close();
            stream.Close();
        }
    }
}
