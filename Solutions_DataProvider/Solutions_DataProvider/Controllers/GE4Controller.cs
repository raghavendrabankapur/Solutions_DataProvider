using Microsoft.AspNetCore.Mvc;
using Solutions_DataProvider.DataProvider;

namespace Solutions_DataProvider.Controllers
{
    [Route("GE4")]
    public class Ge4Controller : Controller
    {
        [HttpPost("{region}")]
        public string AddDetails(string region,[FromBody] dynamic jsonToBeAdded)
        {
            var data = new DataProvider.DataAccess("ge4", region).AddKey(jsonToBeAdded.ToString());
            return data;
        }

        [HttpGet("{region}/{country}")]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ge4", region, country).Get("info");
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

        //[HttpPost("add/{region}/{path}")]
        //public string AddKey(string region, string path, [FromBody] DataAccess.PostData valueToUpdate)
        //{
        //    var data = new DataProvider.DataAccess("ge4", region).AddKey(path,valueToUpdate);
        //    return data;
        //}

        [HttpGet("{region}/{country}/{param}")]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ge4", region, country).Get(param);
            return data;
        }
    }
}