using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskApi2a.Services
{
    public class CurrencyConvertService : ICurrencyConvertService
    {
        public async Task<decimal> Convert(string baseCurrency, string targetCurrency, decimal amount)
        {
            var result = await Rates.GetRate(baseCurrency, targetCurrency, null);
            if (result != null)
            {
                return result.Rates[targetCurrency] * amount;
            }
            return 0;
        }

        public async Task<decimal> Convert(string baseCurrency, string targetCurrency, decimal amount, DateTime date)
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
