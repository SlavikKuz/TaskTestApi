using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskApi2b.Services;

namespace TaskApi2b.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : Controller
    {
        private readonly ILogger<RatesController> _logger;
        private readonly ICurrencyConvertService _service;

        public RatesController(ILogger<RatesController> logger, ICurrencyConvertService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{baseCurrency}/{targetCurrency}/{amount}")]
        public async Task<IActionResult> Get(string baseCurrency, string targetCurrency, decimal amount, DateTime? date)
        {
            if (!InputCheck(baseCurrency, targetCurrency, amount))
            {
                return BadRequest();
            }

            var result = await _service.Convert(baseCurrency, targetCurrency, amount, date);

            if (result == 0)
            {
                return BadRequest();
            }

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
