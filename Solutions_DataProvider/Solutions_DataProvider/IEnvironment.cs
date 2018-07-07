using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions_DataProvider
{
    interface IEnvironment
    {
        [HttpGet]
        string GetEnvironmentDetails(string region, string country, [FromQuery] string param);
        [HttpGet]
        string GetDetails([FromQuery] string region, [FromQuery] string country);
        [HttpGet]
        string GetRegionDetails(string region);
        [HttpPut]
        string AddDetails(string region, [FromBody] dynamic jsonToUpdate);
    }
}
