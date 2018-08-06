using Microsoft.AspNetCore.Mvc;

namespace Solutions_DataProvider.Controllers
{
    [Route("GE4")]
    public class Ge4Controller : Controller, IEnvironment
    {
        [HttpPut("{region}")]
        public string AddDetails(string region,[FromBody] dynamic jsonToBeAdded)
        {
            var resp = new DataProvider.DataAccess("ge4", region).Update(jsonToBeAdded.ToString());
            return resp;
        }

        [HttpGet("{region}/{country}")]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ge4", region, country).Get("info");
            return data;
        }

        [HttpGet("{region}/{country}/{param}")]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ge4", region, country).Get(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ge4", region).Get("");
            return data;
        }

        [HttpPut("{region}/{param}")]
        public string UpdateKey(string region, string param, [FromBody] dynamic valueToUpdate)
        {
            var data = new DataProvider.DataAccess("ge4", region).UpdateKey(param, valueToUpdate.ToString());
            return data;
        }
    }
}