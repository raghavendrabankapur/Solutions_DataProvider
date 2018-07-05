using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Solutions_DataProvider.Controllers
{
    [Route("GE4")]
    public class GE4Controller : Controller, IEnvironment
    {
        [HttpGet("{region}/{country}")]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ge4", region, country).GetKey("info");
            return data;
        }

        [HttpGet("{region}/{country}/{param}")]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ge4", region, country).GetKey(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ge4", region).GetKey("");
            return data;
        }
    }
}