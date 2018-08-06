using Microsoft.AspNetCore.Mvc;

namespace Solutions_DataProvider
{
    internal interface IEnvironment
    {
        [HttpGet]
        string GetEnvironmentDetails(string region, string country, [FromQuery] string param);
        [HttpGet]
        string GetDetails([FromQuery] string region, [FromQuery] string country);
        [HttpGet]
        string GetRegionDetails(string region);
        [HttpPut]
        string AddDetails(string region, [FromBody] dynamic jsonToUpdate);
        [HttpPut]
        string UpdateKey(string region, [FromQuery] string param, [FromBody] dynamic valueToUpdate);
    }
}
