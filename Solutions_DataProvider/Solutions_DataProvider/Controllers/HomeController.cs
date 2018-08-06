using System;
using Microsoft.AspNetCore.Mvc;

namespace Solutions_DataProvider.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private static readonly string Newline = Environment.NewLine;

        private readonly string _returnvalue = $"How to use {Newline} {Newline}" +
            $"URL:/{{environment}}/{{region}}/{{country}}/{{[optional]params}} {Newline}{Newline}" +
            $"environment:ci,ge1,ge2,ge3,ge4,prod {Newline}" +
            $"region:amer,euro,apj {Newline}" +
            $"country:us,uk,de,in,cn, etc{Newline}" +
            $"params:info,urls,users,products";

        [HttpGet]
        public string Data()
        {
            return _returnvalue;
        }
    }
}