using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Solutions_DataProvider.Controllers
{
    [Route("GE2")]
    public class GE2Controller : Controller, IEnvironment
    {
        [HttpPut("{region}")]
        public string AddDetails(string region, [FromBody] dynamic jsonToBeAdded)
        {
            var resp = new DataProvider.DataAccess("ge2", region).Update(jsonToBeAdded.ToString());
            return resp;
        }

        [HttpGet]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ge2", region, country).Get("info");
            return data;
        }

        [HttpGet]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ge2", region, country).Get(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ge2", region).Get("");
            return data;
        }
    }
}