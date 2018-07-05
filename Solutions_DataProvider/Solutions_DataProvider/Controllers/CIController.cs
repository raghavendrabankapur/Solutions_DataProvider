using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Solutions_DataProvider.Controllers
{
    [Route("CI")]
    public class CIController : Controller, IEnvironment
    {
        [HttpGet("{region}/{country}")]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ci", region, country).GetKey("info");
            return data;
        }

        [HttpGet("{region}/{country}/{param}")]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ci", region, country).GetKey(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ci", region).GetKey("");
            return data;
        }
    }
}
