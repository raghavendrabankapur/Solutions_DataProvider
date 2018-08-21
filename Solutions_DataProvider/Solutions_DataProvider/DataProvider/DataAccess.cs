using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Solutions_DataProvider.DataProvider
{
    public class DataAccess
    {
        string _country;
        readonly string _environment;
        readonly string _region;
        private JObject _obj;
        public DataAccess(string environment, string region, string country = null)
        {
            _environment = environment;
            _region = region;
            _country = country != null ? country.ToLower() : string.Empty;
            GetFile();
        }

        private JObject GetObject(string key, JObject fromObj=null)
        {
            if(fromObj == null)
            {
                fromObj = _obj;
            }
            if (fromObj == null)
            {
                throw new Exception("No environment with the region found");
            }

            if (string.IsNullOrEmpty(_country))
            {
                return fromObj;
            }

            var root = fromObj.SelectToken(_country.Replace(":", ".")) as JObject;
            //root = key.Split(':').Aggregate(root, (current, item) => (current[item].GetType() == typeof(JArray) ? (current[item])[0].ToObject<JObject>() : (current[item]).ToObject<JObject>()));
            return root;
        }

        public string Get(string key)
        {
            var root = GetObject(key);
            var val = root != null ? root.ToString() : throw new Exception($"Cound not find key {key}");
            return val;
        }

        public string Update(string jsonToBeUpdated)
        {
            var path = GetFilePath();
            lock (new object())
            {
                File.WriteAllText(path, jsonToBeUpdated);
            }
            return "Environment updated";
        }

        public string UpdateKey(string key, string value)
        {
            var json = File.ReadAllText(GetFilePath());
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            if (string.IsNullOrEmpty(_country))
            {
                var countrycodeToken = JObject.Parse(json).SelectTokens("..countrycode");
                var values = countrycodeToken.Select(x => (x as JValue)?.Value).ToList();
                if (values.Count >= 1)
                {
                    _country = values[0].ToString();
                }
            }

            var token = ((JObject) jsonObj).SelectToken($"{_country}.{key.Replace(":", ".")}");

            if (token.Parent is JProperty prop) prop.Value = value;

            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(GetFilePath(), output);
            return "Updated the key";
        }

        public string AddKey(string data)
        {
            var json = File.ReadAllText(GetFilePath());
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            dynamic toupdate = JsonConvert.DeserializeObject(data);

            var allkeys = toupdate.key.ToString().Split(":");

            JToken ptkn = null;
            var path=string.Empty;

            foreach (var k in allkeys)
            {
                path = $"{path}.{k}";
                var ctkn = ((JObject) jsonObj).SelectToken(path);
                if (ctkn == null)
                {
                    if (ptkn.GetType() == typeof(JObject))
                    {
                        ((JObject) ptkn).Add(new JProperty(k, toupdate.value));
                    }

                    break;
                }
                else if (ctkn.GetType() == typeof(JArray) && ((Newtonsoft.Json.Linq.JProperty)ctkn.Parent).Name.ToLower().Contains("users"))
                {
                    if (((JArray) ctkn).Count >= 0)
                    {
                        ((JArray)ctkn).Insert(((JArray)ctkn).Count-1, new JObject(toupdate.value.ToString()));
                    }
                }

                ptkn = ctkn;
            }

            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(GetFilePath(), output);

            return "Key added";
        }

        private string GetFilePath()
        {
            var isWindows = RuntimeInformation
                                              .IsOSPlatform(OSPlatform.Windows);
            Console.WriteLine($"Platfomr detected is {RuntimeInformation.OSDescription}");
            var filename = isWindows ? $"{Directory.GetCurrentDirectory()}\\DataProvider\\Data\\{_environment.ToLower()}\\{_region.ToLower()}.json" : $"{Directory.GetCurrentDirectory()}/DataProvider/Data/{_environment.ToLower()}/{_region.ToLower()}.json";
            return filename;
        }

        private void GetFile()
        {
            var stream = File.OpenText(GetFilePath());

            var reader = new JsonTextReader(stream);

            _obj = (JObject)JToken.ReadFrom(reader);
            reader.Close();
            stream.Close();
        }

        public class PostData
        {
            public string key { get; set; }
            public string value { get; set; }
        }
    }
}
