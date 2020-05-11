using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskApi2d.Ui.Models;
using TaskApi2d.UI.Models;

namespace TaskApi2d.UI.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new RatesClient().Client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(DateTime dateFrom, DateTime dateTo)
        {
            HttpResponseMessage response = await client.GetAsync("/Rates/" + dateFrom.ToString("yyyy.MM.dd")
                                                               + "/" + dateTo.ToString("yyyy.MM.dd"));

            if (!response.IsSuccessStatusCode)
            {
                return Content("An error occured while connecting to web api");
            }

            string contentresult = await response.Content.ReadAsStringAsync();
            
            IEnumerable<ExchangeRateViewModel> rates = 
                JsonConvert.DeserializeObject<IEnumerable<ExchangeRateViewModel>>(contentresult);

            var convert = rates.Select(x => new { date = DatestampToString(x.Timestamp), rate = x.Rate }).ToList();

            return Json(convert);
        }

        public static string DatestampToString(int datestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(datestamp).ToLocalTime().ToShortDateString();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
