using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TaskApi2c.Services;

namespace TaskApi2c.Controllers
{
    //[Authorize] as an idea, Identity
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly ICurrencyConvertService _service;
        private readonly IDbService _dbService;

        public RatesController(ILogger<RatesController> logger, 
                               ICurrencyConvertService service,
                               IDbService dbService)
        {
            _logger = logger;
            _service = service;
            _dbService = dbService;
        }

        [HttpGet("{baseCurrency}/{targetCurrency}/{amount}/{date:DateTime?}")]
        public async Task<IActionResult> GetRate(string baseCurrency, string targetCurrency, decimal amount, DateTime? date)
        {
            if (!InputCheck(baseCurrency, targetCurrency, amount))
            {
                return BadRequest();
            }

            var result = await _service.Convert(baseCurrency, targetCurrency, amount, date);
            return Ok(result);
        }

        //dates passed as arguments are already valid
        [HttpGet("{dateFrom}/{dateTo}")]
        public IActionResult GetFromTo(DateTime dateFrom, DateTime dateTo)
        {
            var result = _dbService.GetFromTo(dateFrom, dateTo)
                .Select(x => x.Rate).ToList();

           return Ok(result);
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
    }
}
