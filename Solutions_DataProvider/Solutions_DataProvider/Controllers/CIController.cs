using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Solutions_DataProvider.Controllers
{
    [Route("CI")]
    public class CIController : Controller, IEnvironment
    {
        [HttpPut("{region}")]
        public string AddDetails(string region, [FromBody] dynamic jsonToBeAdded)
        {
            var resp = new DataProvider.DataAccess("ci", region).Update(jsonToBeAdded.ToString());
            return resp;
        }

        [HttpGet("{region}/{country}")]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ci", region, country).Get("info");
            return data;
        }

        [HttpGet("{region}/{country}/{param}")]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ci", region, country).Get(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ci", region).Get("");
            return data;
        }

        public string UpdateKey(string region, [FromQuery] string param, [FromBody] dynamic valueToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
