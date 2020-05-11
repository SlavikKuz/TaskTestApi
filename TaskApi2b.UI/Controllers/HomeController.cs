using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskApi2b.UI.Models;

namespace TaskApi2b.UI.Controllers
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserInputViewModel inputData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please enter a valid data.";
                return View();
            }

            inputData.BaseCurrency = inputData.BaseCurrency.ToUpper();
            inputData.TargetCurrency = inputData.TargetCurrency.ToUpper();

            if (!decimal.TryParse(inputData.Amount.Replace('.', ','), out decimal amount))
            {
                ViewBag.Message = "Please enter a valid amount.";
                return View();
            }

            //no localization ./,
            string request = $"/Rates/{inputData.BaseCurrency}/{inputData.TargetCurrency}/{amount.ToString().Replace(",",".")}"
                + (inputData.Date != null ? $"/{inputData.Date.Value.ToString("yyyy.MM.dd")}" : "");

            HttpResponseMessage response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Something's gone wrong :(";
                return View();
            }

            var result = await response.Content.ReadAsStringAsync();

            ViewBag.Message = "Converted amount: ";
            ViewBag.Amount = result + " " + inputData.TargetCurrency;
            return View();
        }

        public static bool InputCheck(string baseCurrency, string targetCurrency, decimal amount)
        {
            if (baseCurrency.ToCharArray().Count() == 3 && IsAllUppercase(baseCurrency)
                && targetCurrency.ToCharArray().Count() == 3 && IsAllUppercase(targetCurrency)
                && amount != 0)
                return true;
            else
                return false;
        }

        public static bool IsAllUppercase(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }
            return true;
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
