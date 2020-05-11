using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskApi2d.Models;

namespace TaskApi2d.Services
{
    public class CurrencyConvertService : ICurrencyConvertService
    {
        public async Task<decimal> Convert(string baseCurrency, string targetCurrency, decimal amount, DateTime? date)
        {
            var result = await Rates.GetRate(baseCurrency, targetCurrency, date);
            if (result != null)
            {
                return result.Rates[targetCurrency] * amount;
            }
            return 0;
        }
    }
}
