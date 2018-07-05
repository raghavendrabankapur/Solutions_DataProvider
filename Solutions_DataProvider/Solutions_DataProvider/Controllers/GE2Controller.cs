﻿using System;
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
        [HttpGet]
        public string GetDetails(string region, string country)
        {
            var data = new DataProvider.DataAccess("ge2", region, country).GetKey("info");
            return data;
        }

        [HttpGet]
        public string GetEnvironmentDetails(string region, string country, string param)
        {
            var data = new DataProvider.DataAccess("ge2", region, country).GetKey(param);
            return data;
        }

        [HttpGet("{region}")]
        public string GetRegionDetails(string region)
        {
            var data = new DataProvider.DataAccess("ge2", region).GetKey("");
            return data;
        }
    }
}