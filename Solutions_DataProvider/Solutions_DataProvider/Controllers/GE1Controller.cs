﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Solutions_DataProvider.Controllers
{
    [Route("GE1")]
    public class GE1Controller : Controller,IEnvironment
    {
        [HttpGet("{region}/{country}")]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ge1", region, country).GetKey("info");
            return data;
        }

        [HttpGet("{region}/{country}/{param}")]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ge1", region, country).GetKey(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ge1", region).GetKey("");
            return data;
        }
    }
}
