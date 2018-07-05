using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Solutions_DataProvider.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        static string newline = Environment.NewLine;
        string returnvalue = $"How to use {newline} {newline}" +
            $"URL:/{{environment}}/{{region}}/{{country}}/{{[optional]params}} {newline}{newline}" +
            $"environment:ci,ge1,ge2,ge3,ge4,prod {newline}" +
            $"region:amer,euro,apj {newline}" +
            $"country:us,uk,de,in,cn, etc{newline}" +
            $"params:info,urls,users,products";

        [HttpGet]
        public string Data()
        {
            return returnvalue;
        }
    }
}