﻿using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Solutions_DataProvider.DataProvider
{
    public class DataAccess
    {
        readonly string _country;
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

            var root = fromObj[_country] as JObject;
            root = key.Split(':').Aggregate(root, (current, item) => (current[item].GetType() == typeof(JArray) ? (current[item])[0].ToObject<JObject>() : (current[item]).ToObject<JObject>()));
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

            if (!string.IsNullOrEmpty(_country))
            {
                jsonObj = jsonObj[_country];
            }

            foreach (var item in key.Split(':'))
            {
                jsonObj = jsonObj[item];
            }

            jsonObj = "new password";
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(GetFilePath(), output);
            return "Updated the key";
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
    }
}
