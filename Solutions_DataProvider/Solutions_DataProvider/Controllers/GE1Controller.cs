using Microsoft.AspNetCore.Mvc;

namespace Solutions_DataProvider.Controllers
{
    [Route("GE1")]
    public class Ge1Controller : Controller,IEnvironment
    {
        [HttpPut("{region}")]
        public string AddDetails(string region, [FromBody] dynamic jsonToBeAdded)
        {
            var resp = new DataProvider.DataAccess("ge1", region).Update(jsonToBeAdded.ToString());
            return resp;
        }

        [HttpGet("{region}/{country}")]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ge1", region, country).Get("info");
            return data;
        }

        [HttpGet("{region}/{country}/{param}")]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ge1", region, country).Get(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ge1", region).Get("");
            return data;
        }

        public string UpdateKey(string region, [FromQuery] string param, [FromBody] dynamic valueToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
